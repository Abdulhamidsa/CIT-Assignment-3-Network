using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using Assignment3.DTOS;
using Assignment3.Services;
using Assignment3.Utilities;

namespace Assignment3
{
    public class CITServer
    {
        // Create instances of helper classes we'll need
        private readonly RequestValidator validator = new();
        private readonly UrlParser parser = new();
        private readonly CategoryService categories = new();

        // Main server loop - keeps listening for connections
        public void Run()
        {
            var server = new TcpListener(IPAddress.Loopback, 5000);
            server.Start();
            Console.WriteLine("Server running on port 5000");

            // Keep server running forever
            while (true)
            {
                if (!server.Pending()) { Thread.Sleep(50); continue; }
                using var client = server.AcceptTcpClient();
                HandleClient(client);
            }
        }





        private void HandleClient(TcpClient client)
        {
            using var stream = client.GetStream();
            using var writer = new StreamWriter(stream, new UTF8Encoding(false)) { AutoFlush = true };

            string raw = ReadRequest(stream);
            if (string.IsNullOrWhiteSpace(raw))
            {
                Send(writer, new Response { Status = "4 Bad Request" });
                return;
            }

            Request? req;
            try
            {
                req = JsonSerializer.Deserialize<Request>(raw,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch
            {
                Send(writer, new Response { Status = "4 Bad Request" });
                return;
            }

            var check = validator.ValidateRequest(req);
            if (check.Status != "1 Ok") { Send(writer, check); return; }

            if (req.Method?.ToLower() != "echo" && !parser.ParseUrl(req.Path ?? ""))
            { Send(writer, new Response { Status = "5 Not found" }); return; }

            switch (req.Method?.ToLower())
            {
                case "echo":
                    Send(writer, new Response { Status = "1 Ok", Body = req.Body });
                    break;

                case "read":
                    HandleRead(writer);
                    break;

                case "create":
                    HandleCreate(writer, req);
                    break;

                case "update":
                    HandleUpdate(writer, req);
                    break;

                case "delete":
                    HandleDelete(writer);
                    break;

                default:
                    Send(writer, new Response { Status = "4 illegal method" });
                    break;
            }
        }

        private static string ReadRequest(NetworkStream stream)
{
    try
    {
        var buffer = new byte[4096];
        using var mem = new MemoryStream();
        stream.ReadTimeout = 1000;

        int read;
        do
        {
            read = stream.Read(buffer, 0, buffer.Length);
            if (read > 0) mem.Write(buffer, 0, read);
            Thread.Sleep(30);
        } while (stream.DataAvailable);

        return Encoding.UTF8.GetString(mem.ToArray()).Trim('\0', '\r', '\n');
    }
    catch (IOException)
    {
        // connection timed out or client didn’t send anything
        return string.Empty;
    }
    catch (SocketException)
    {
        // same idea: connection failed mid-read
        return string.Empty;
    }
}


        private void HandleRead(StreamWriter w)
        {
            if (parser.Path != "/api/categories") { Send(w, new Response { Status = "5 Not found" }); return; }

            if (parser.HasId)
            {
                if (!int.TryParse(parser.Id, out int id))
                {
                    Send(w, new Response { Status = "4 Bad Request" });
                    return;
                }
                
                var c = categories.GetCategory(id);
                if (c == null) { Send(w, new Response { Status = "5 Not found" }); return; }
                Send(w, new Response { Status = "1 Ok", Body = JsonSerializer.Serialize(new { cid = c.Id, name = c.Name }) });
                return;
            }

            var list = categories.GetCategories().ConvertAll(c => new { cid = c.Id, name = c.Name });
            Send(w, new Response { Status = "1 Ok", Body = JsonSerializer.Serialize(list) });
        }

        private static void Send(StreamWriter w, Response res)
        {
            w.Write(JsonSerializer.Serialize(res, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }
    }
}

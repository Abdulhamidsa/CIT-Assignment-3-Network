using Assignment3.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assignment3.Services
{
    public class RequestValidator
    {
        
        private static readonly HashSet<string> AllowedMethods =
            new(StringComparer.Ordinal) { "create", "read", "update", "delete", "echo" };

        public Response ValidateRequest(Request request)
        {
            var problems = new List<string>();

            // --- Presence checks (missing) ---
            var method = request.Method?.Trim();
            var path = request.Path?.Trim();
            var date = request.Date?.Trim();
            var body = request.Body; // don't trim body; spaces may be valid JSON

            if (string.IsNullOrWhiteSpace(method))
            {
                problems.Add("missing method");
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                problems.Add("missing path");
            }

            if (string.IsNullOrWhiteSpace(date))
            {
                problems.Add("missing date");
            }

            // --- Legality/format checks (only if present) ---
            if (!string.IsNullOrWhiteSpace(method) && !AllowedMethods.Contains(method))
            {
                problems.Add("illegal method");
            }

            if (!string.IsNullOrWhiteSpace(date) && !long.TryParse(date, out _))
            {
                problems.Add("illegal date");
            }

            // --- Body rules ---
            // For create/update/echo → body required
            if (!string.IsNullOrWhiteSpace(method) &&
                (method == "create" || method == "update" || method == "echo"))
            {
                if (string.IsNullOrWhiteSpace(body))
                {
                    problems.Add("missing body");
                }
                else
                {
                    // For create & update → body must be valid JSON
                    if (method == "create" || method == "update")
                    {
                        if (!IsValidJson(body))
                        {
                            problems.Add("illegal body");
                        }
                    }
                    // For echo → any non-empty string body is fine (no JSON requirement)
                }
            }

            // --- Build the response ---
            if (problems.Count == 0)
            {
                return new Response { Status = "1 Ok" };
            }

            // Tests use Assert.Contains("..."), so including all reasons is fine.
            return new Response { Status = "4 " + string.Join(", ", problems) };
        }

        private static bool IsValidJson(string text)
        {
            try
            {
                using var _ = JsonDocument.Parse(text);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}


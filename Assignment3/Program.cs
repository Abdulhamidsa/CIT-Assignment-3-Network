using System;

namespace Assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Web Service :-)");
    
            var server = new CITServer();
            server.Run();
        }
    }
}

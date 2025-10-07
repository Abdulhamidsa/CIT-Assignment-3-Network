using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.DTOS
{
    public class Request
    {
        public string? Method { get; set; }
        public string? Path { get; set; }
        public string? Date { get; set; }   // tests pass date as a string
        public string? Body { get; set; }   // tests pass body as a string (JSON text or plain text)
    }
}

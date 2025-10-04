using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.Utilities
{
    public class UrlParser
    {
        public bool HasId { get; set; }
        public string Id { get; set; } = "";
        public string Path { get; set; } = "";


        public bool ParseUrl(string url)
        {
            // 1) Quick sanity checks (robustness; not strictly required by the test)
            if (string.IsNullOrWhiteSpace(url)) return false;

            // 2) Split by slash and remove empty entries (so leading "/" doesn't give an empty first segment)
            var parts = url.Split('/', StringSplitOptions.RemoveEmptyEntries);

            // We only care about two shapes for Part I tests:
            //   Shape A: /api/categories
            //   Shape B: /api/categories/<id>
            if (parts.Length == 2)
            {
                // Base path only
                Path = "/" + parts[0] + "/" + parts[1];  // preserve leading slash
                HasId = false;
                Id = "";
                return true;
            }

            if (parts.Length == 3)
            {
                // Base path + trailing id
                Path = "/" + parts[0] + "/" + parts[1];
                HasId = true;
                Id = parts[2]; // treat last segment as the id (string)
                return true;
            }

            // Any other shape isn't considered valid for these tests
            return false;
        }
    }


}
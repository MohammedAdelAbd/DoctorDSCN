using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Response
    {
        public bool seccess { get; set; }
        public string message { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }

    }
}

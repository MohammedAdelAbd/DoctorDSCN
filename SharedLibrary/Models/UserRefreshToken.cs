using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class UserRefreshToken
    {
        public Guid Id { get; set; } = new Guid();
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExparitionDate { get; set; }
    }
}

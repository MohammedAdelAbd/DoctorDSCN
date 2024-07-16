using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
     
    public class UserInfo
    {
        
        public Guid UserInfoId { get; set; } = Guid.NewGuid();
        public string InfoName { get; set; }
        public string InfoAdress { get; set; }
        public string InfoPhone { get; set; }
        [Required,EmailAddress]
        public string InfoEmail { get; set; }
        public string InfoImage { get; set; }
        public string InfoNameEnglish { get; set; }
        public Guid UserID { get; set; }

        [ForeignKey(nameof(UserID))]
        public User User { get; set; }
    }
}

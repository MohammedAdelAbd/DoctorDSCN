using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; } = Guid.NewGuid();

        [Required, DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required,DataType(DataType.Password)]
        public string UserPassword { get; set; } 


        
    }
}

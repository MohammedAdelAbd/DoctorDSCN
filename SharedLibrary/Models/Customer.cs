using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Customer
    {
        [Key]
        public Guid customerId { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Customer Certificate is mandatory")]
        [MaxLength(100),MinLength(2)]
        public string customerCertificate { get; set; }
        [Required]
        [MaxLength(100)]
        public string customerAddressJob { get; set; }
        [Required]
        [MaxLength(100)]
        public string customerSpecilaztion { get; set; }
        [Required]
        [MaxLength(50)]
        public string customerScientificTitle { get; set; }
        [Required]
        public bool NewCustomer { get; set; }
        [ForeignKey(nameof(UserID))]
        public Guid UserID { get; set; }

         
         
    }
}

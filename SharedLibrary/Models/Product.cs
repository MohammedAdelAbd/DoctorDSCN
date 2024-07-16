using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Product
    { 
            [Key]
            public Guid productId { get; set; } = Guid.NewGuid();
            public string productName { get; set; }
            public string productType { get; set; }

         
             
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class CustomerProduct
    {
        public Guid CustomerProductId { get; set; }
        public Guid productId { get; set; }
        public Guid customerId { get; set; }

        [ForeignKey(nameof(productId))]
        public Product? userProduct { get; set; }
         
        [ForeignKey(nameof(customerId))]
        public Customer? userCustomer { get; set; }
         
        
         
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Models;
using WebApplication2.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCustomerController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public UserCustomerController(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        // GET: api/<UserCustomerController>
         
        [HttpGet]
        public IEnumerable<Customer> GetAllUserCustomer()
        {
            return _appDbContext.Customers.ToList();
        }

        // GET api/<UserCustomerController>/5
        [HttpGet("{id}")]
        public async Task<Customer> GetUserById(string id)
        {
            var result = _appDbContext.Customers.FirstOrDefault(x => x.customerId == Guid.Parse(id));
            return result;
        }
        // GET api/<CustomerProductController>/userproductswithoutcustomerproducts
        [HttpGet("usercustomerwithoutproducts")]
        public List<Customer> GetUserCustomerWithoutProducts()
        { 
            var query = from customer in _appDbContext.Customers
                        join customerProduct in _appDbContext.CustomerProducts
                        on customer.customerId equals customerProduct.customerId into customerProductsGroup
                        from customerProduct in customerProductsGroup.DefaultIfEmpty()
                        where customerProduct == null
                        select new Customer
                        {
                              customerId = customer.customerId,
                              customerCertificate = customer.customerCertificate,
                              customerAddressJob = customer.customerAddressJob,
                              customerScientificTitle = customer.customerScientificTitle,
                              customerSpecilaztion = customer.customerSpecilaztion,
                              NewCustomer = customer.NewCustomer,
                              UserID = customer.UserID

                        };

            var result = query.ToList();
            return result;
        }
        // POST api/<UserCustomerController>
        [HttpPost]
        public bool  Post(Customer userCustomer)
        {

            if (userCustomer == null || userCustomer.UserID == Guid.Empty)
            {
                return false;
            }

            var user = _appDbContext.Users.Where(x => x.UserID == userCustomer.UserID).FirstOrDefault();
            if (user == null)
            {
                return false;
            }

             



            _appDbContext.Customers.Add(userCustomer);
                _appDbContext.SaveChanges();
            return true;
        }

        // PUT api/<UserCustomerController>/5
        [HttpPut("{id}")]
        public bool Put(string id, Customer userCustomer)
        {

            var customer = _appDbContext.Customers.FirstOrDefault(x => x.customerId == Guid.Parse(id));
            if (customer == null)
            {
                return false;
            }
            if (customer != null)
            {

                customer.customerCertificate = userCustomer.customerCertificate;
                customer.customerAddressJob = userCustomer.customerAddressJob;
                customer.customerScientificTitle = userCustomer.customerScientificTitle;
                customer.customerSpecilaztion = userCustomer.customerSpecilaztion;
                customer.NewCustomer = userCustomer.NewCustomer;

                _appDbContext.SaveChanges();

            }


            return true;
        }

        // DELETE api/<UserCustomerController>/5
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            var customer = _appDbContext.Customers.FirstOrDefault(x => x.customerId == Guid.Parse(id));
            if (customer == null)
            {
                return false;
            }
            _appDbContext.Customers.Remove(customer);
            _appDbContext.SaveChangesAsync();
            return true;

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using WebApplication2.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerProductController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CustomerProductController(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        // GET: api/<CustomerProductController>
        [HttpGet]
        public IEnumerable<CustomerProduct> GetAllCustomerProduct()
        {
            return _appDbContext.CustomerProducts.Include(x=>x.userProduct).Include(u=>u.userCustomer).ToList();
        }

        // GET api/<CustomerProductController>/5
        [HttpGet("{id}")]
        public CustomerProduct GetCustomerProductById(string id)
        {
            var userResult = _appDbContext.CustomerProducts.Where(x => x.CustomerProductId == Guid.Parse(id)).FirstOrDefault();

            return userResult;
        }

        // POST api/<CustomerProductController>
        [HttpPost]
        public void Post(CustomerProduct customerProduct)
        {


            _appDbContext.CustomerProducts.Add(customerProduct);
            _appDbContext.SaveChanges();
        }

        // PUT api/<CustomerProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        
    }
}

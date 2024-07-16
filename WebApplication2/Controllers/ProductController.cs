using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Models;
using WebApplication2.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public ProductController(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        // GET: api/<productController>
        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            return _appDbContext.Products.ToList();
        }

        // GET api/<productController>/5
        [HttpGet("{id}")]
        public Product GetProductById(string id)
        {
            return _appDbContext.Products.Where(x=>x.productId ==Guid.Parse( id)).FirstOrDefault();
        }
        // GET api/<CustomerProductController>/userproductswithoutcustomerproducts
        [HttpGet("userproductswithoutcustomerproducts")]
        public List<Product> GetUserProductsWithoutCustomerProducts()
        {
            var query = from userProduct in _appDbContext.Products
                        join customerProduct in _appDbContext.CustomerProducts
                        on userProduct.productId equals customerProduct.productId into customerProductsGroup
                        from customerProduct in customerProductsGroup.DefaultIfEmpty()
                        where customerProduct == null
                        select new Product
                        {
                            productId = userProduct.productId,
                            productName = userProduct.productName,
                            productType = userProduct.productType
                        };

            var result = query.ToList();
            return result;

        }
        // POST api/<productController>
        [HttpPost]
        public void Post(Product userProduct)
        {

            _appDbContext.Products.Add(userProduct);
            _appDbContext.SaveChangesAsync();
        }

        // PUT api/<productController>/5
        [HttpPut("{id}")]
        public bool Put(string id, Product userProduct)
        {
            var product = _appDbContext.Products.FirstOrDefault(x => x.productId == Guid.Parse(id));
            if (product == null)
            {
                return false;
            }
            if (product != null)
            {
                 
                product.productName = userProduct.productName;
                product.productType = userProduct.productType;
                
                _appDbContext.SaveChanges();
                 
            }

            
            return true;

        }

        // DELETE api/<productController>/5
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            var product = _appDbContext.Products.FirstOrDefault(x => x.productId == Guid.Parse(id));

            if (product == null)
            {
                return false;
            }
            _appDbContext.Products.Remove(product);
            _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}

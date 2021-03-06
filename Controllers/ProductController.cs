using GeneralStoreAPI.Data;
using GeneralStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace GeneralStoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private GeneralStoreMainDbContext _db;
        public ProductController(GeneralStoreMainDbContext db) {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm]ProductEdit newProduct) {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            Product product = new Product() {
                Name = newProduct.Name,
                Price = newProduct.Price,
                QuantityInStock = newProduct.QuantityInStock,
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts() {
            var products = await _db.Products.ToListAsync();
            return Ok(products);
        }
    }
}
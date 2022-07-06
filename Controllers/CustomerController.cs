using GeneralStoreAPI.Data;
using GeneralStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace GeneralStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private GeneralStoreMainDbContext _db;
        public CustomerController(GeneralStoreMainDbContext db) {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerEdit newCustomer) {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            Customer customer = new Customer() {
                Name = newCustomer.Name,
                Email = newCustomer.Email,
            };

            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers() {
            var customers = await _db.Customers.ToListAsync();
            return Ok(customers);
        }
    }
}
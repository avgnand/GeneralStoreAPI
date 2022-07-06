using GeneralStoreAPI.Data;
using GeneralStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace GeneralStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private GeneralStoreMainDbContext _db;
        public TransactionController(GeneralStoreMainDbContext db) {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromForm] TransactionEdit newTransaction) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            Transaction transaction = new Transaction() {
                ProductId = newTransaction.ProductId,
                CustomerId = newTransaction.CustomerId,
                Quantity = newTransaction.Quantity,
                DateOfTransaction = DateTime.Now,
            };

            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions() {
            var transactions = await _db.Transactions.ToListAsync();
            return Ok(transactions);
        }
    }
}
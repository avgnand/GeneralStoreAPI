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

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTransaction([FromForm] TransactionEdit model, [FromRoute] int id) {
            var transaction = await _db.Transactions.FindAsync(id);
            if (transaction == null) {
                return NotFound();
            }
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if (model.ProductId != default) {
                transaction.ProductId = model.ProductId;
            }
            if (model.CustomerId != default) {
                transaction.CustomerId = model.CustomerId;
            }
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] int id) {
            var transaction = await _db.Transactions.FindAsync(id);
            if (transaction == null) {
                return NotFound();
            }
            _db.Transactions.Remove(transaction);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
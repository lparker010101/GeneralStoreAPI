using GeneralStoreAPI.Models;
using GeneralStoreAPI.Models.Data_POCOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class TransactionController : ApiController
    {
        private readonly PDC_Context _context = new PDC_Context();  // Made _context a field because there will be many controller methods that must access the PDC_Context.

        [HttpPost]
        public async Task<IHttpActionResult> Post(Transaction transactionObject)
        {
            decimal totalCost;
            if (transactionObject is null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the product
            var product = await _context.Products.FindAsync(transactionObject.ProductSKU);

            //Verify that the product is in stock
            if (product.IsInStock)
            {
                totalCost = (decimal)(transactionObject.ItemCount * product.Cost);
                product.NumberInInventory -= transactionObject.ItemCount;
                if (product.NumberInInventory > 0)
                {
                    transactionObject.DateOfTransaction = DateTime.Now;
                    _context.Transactions.Add(transactionObject);

                    if (await _context.SaveChangesAsync() > 0)
                    {
                        return Ok($"Transaction: {transactionObject.ID} was added to the database.\n" +
                            $"Total Cost ${totalCost}.");
                    }
                }
                return BadRequest("Item out of stock");
            }
            return InternalServerError();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {                                                                 //ToListAsync creates a List<T> from an IQueryable by enumerating it asynchronously.
            var transactions = await _context.Transactions.ToListAsync(); //ToListAsync means ToList asynchronous. It can execute without crashing the application's main thread.
            return Ok(transactions);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri] int ID)
        {
            var transaction = await _context.Transactions.SingleOrDefaultAsync(t => t.ID == ID); 
            if (transaction is null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }
    }
}



// Required Endpoints:
// Create(POST)
// When creating Transactions, make sure to:
//Verify that the product is in stock
//Check that there is enough product to complete the Transaction  int ItemCount
//Remove the Products that were bought
// Get All Transactions (GET)
// Get a Transaction by its ID (GET)

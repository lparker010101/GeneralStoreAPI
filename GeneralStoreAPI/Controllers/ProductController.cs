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
    public class ProductController : ApiController
    {
        private readonly PDC_Context _context = new PDC_Context();

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Product productObject)
        {
            if (productObject is null)
            {
                return BadRequest("Product not found.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _context.Products.Add(productObject);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok($"Product: {productObject.SKU} was added to the database.");
            }
            return InternalServerError();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get ()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("api/product/{sku}")] // Like a helper method to get a specific product by sku.  
        public async Task<IHttpActionResult> Get ([FromUri] string SKU)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.SKU == SKU);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}

// Required Endpoints:
// Create(POST)
// Get All Products (GET)
// Get a Product by its ID (GET)

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
    public class CustomerController : ApiController
    {
        private readonly PDC_Context _context = new PDC_Context();

        // POST (create method)
        // api/GeneralStore
        // IHttpActionResult is an interface that will encapsulate all of our response types provided by our API controller class. 
        // Get information from the body of html or something else.  The contents of the form will be associated with the properties of customer.
        
        [HttpPost]
        public async Task<IHttpActionResult> Post( [FromBody] Customer customerObject)  
        {
            if (customerObject is null)
            {
                return BadRequest("Null Value ERROR!");  // Lines 25-28: Check to see if they put in any data within the body of the html doc.  If they didn't put anything in there, look at next lines.    
            }
            if (!ModelState.IsValid) // If the model state is valid is false with crazy data entered, return badrequest and return crazy data within the document.
            {
                return BadRequest(ModelState);   
            }
            

           // If above lines pass, we will try to assign or cash in a specific customer based on the ID of the customer that lives in the DbSet context for Customers.
            _context.Customers.Add(customerObject);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok($"Customer: {customerObject.FullName} was Added to the datebase.");
            }
            return InternalServerError();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri] int ID)
        {
            // retrieves a single customer from the _context.customers with the matching ID value
            // If no customer is found the 'Default' is null
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.ID == ID); // Seeing if equal to ID that person passed in.  
            if (customer is null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put ([FromUri] int id, [FromBody] Customer newCustomerData)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            if (id != newCustomerData.ID)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // api/customer/38 -> John Doe
            var customer = await _context.Customers.FindAsync(id);  // Check this _context should return a customer!!!!!

            if (customer is null)
            {
                return NotFound();
            }
            // We update old object data from here.  
            customer.FirstName = newCustomerData.FirstName;
            customer.LastName = newCustomerData.LastName;


            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete ([FromUri] int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                if (await _context.SaveChangesAsync() == 1)
                {
                    return Ok();
                }
            }
            return InternalServerError();
        }
    }
}

// Rewatch Terry's lesson from August 5 @ 2:20.

// Customer
// Create (Post)
// Get All Customers (GET)
// Get a Customer by its ID (GET)
// Update an existing Customer by its ID (PUT)
// Delete an existing Customer by its ID (DELETE)

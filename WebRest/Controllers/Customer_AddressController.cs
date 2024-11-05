using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRestEF.EF.Data;
using WebRestEF.EF.Models;

namespace WebRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public CustomerAddressController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/Customer_Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerAddress>>> GetCustomer_Address() 
        {
            return await _context.CustomerAddresses.ToListAsync();
        }

        // GET: api/Customer_Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerAddress>> GetCustomer_Address(string id)
        {
            var customer_address = await _context.CustomerAddresses.FindAsync(id);

            if (customer_address == null)
            {
                return NotFound();
            }

            return customer_address;
        }

        // PUT: api/Customer_Addresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer_Address(string id, CustomerAddress customer_address)
        {
            if (id != customer_address.CustomerAddressId)
            {
                return BadRequest();
            }

            _context.Entry(customer_address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Customer_AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customer_Addresses
        [HttpPost]
        public async Task<ActionResult<CustomerAddress>> PostCustomer_Address(CustomerAddress customer_address)
        {
            _context.CustomerAddresses.Add(customer_address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer_Address", new { id = customer_address.CustomerAddressId }, customer_address);
        }

        // DELETE: api/Customer_Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer_Address(string id)
        {
            var customer_address = await _context.CustomerAddresses.FindAsync(id);
            if (customer_address == null)
            {
                return NotFound();
            }

            _context.CustomerAddresses.Remove(customer_address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Customer_AddressExists(string id)
        {
            return _context.CustomerAddresses.Any(e => e.CustomerAddressId == id);
        }
    }
}

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
    public class AddressTypeController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public AddressTypeController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/Address_Type
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressType>>> GetAddress_Type()
        {
            return await _context.AddressTypes.ToListAsync();
        }

        // GET: api/Address_Type/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressType>> GetAddress_Type(string id)
        {
            var addressType = await _context.AddressTypes.FindAsync(id);

            if (addressType == null)
            {
                return NotFound();
            }

            return addressType;
        }

        // PUT: api/Address_Type/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress_Type(string id, AddressType addressType)
        {
            if (id != addressType.AddressTypeId)
            {
                return BadRequest();
            }

            _context.Entry(addressType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Address_TypeExists(id))
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

        // POST: api/Address_Type
        [HttpPost]
        public async Task<ActionResult<AddressType>> PostAddress_Type(AddressType addressType)
        {
            _context.AddressTypes.Add(addressType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress_Type", new { id = addressType.AddressTypeId }, addressType);
        }

        // DELETE: api/Address_Type/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress_Type(string id)
        {
            var addressType = await _context.AddressTypes.FindAsync(id);
            if (addressType == null)
            {
                return NotFound();
            }

            _context.AddressTypes.Remove(addressType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Address_TypeExists(string id)
        {
            return _context.AddressTypes.Any(e => e.AddressTypeId == id);
        }
    }
}

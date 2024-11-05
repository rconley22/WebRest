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
    public class OrderStateController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public OrderStateController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrderState
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderState>>> GetOrderStates()
        {
            return await _context.OrderStates.ToListAsync();
        }

        // GET: api/OrderState/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderState>> GetOrderState(string id)
        {
            var orderState = await _context.OrderStates.FindAsync(id);

            if (orderState == null)
            {
                return NotFound();
            }

            return orderState;
        }

        // PUT: api/OrderState/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderState(string id, OrderState orderState)
        {
            if (id != orderState.OrderStateId) // Assuming OrderStateId is the key
            {
                return BadRequest();
            }

            _context.Entry(orderState).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStateExists(id))
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

        // POST: api/OrderState
        [HttpPost]
        public async Task<ActionResult<OrderState>> PostOrderState(OrderState orderState)
        {
            _context.OrderStates.Add(orderState);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderState", new { id = orderState.OrderStateId }, orderState);
        }

        // DELETE: api/OrderState/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderState(string id)
        {
            var orderState = await _context.OrderStates.FindAsync(id);
            if (orderState == null)
            {
                return NotFound();
            }

            _context.OrderStates.Remove(orderState);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderStateExists(string id)
        {
            return _context.OrderStates.Any(e => e.OrderStateId == id);
        }
    }
}

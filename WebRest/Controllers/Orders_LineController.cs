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
    public class OrderLineController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public OrderLineController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrderLine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersLine>>> GetOrderLines()
        {
            return await _context.OrdersLines.ToListAsync();
        }

        // GET: api/OrderLine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdersLine>> GetOrderLine(string id)
        {
            var orderLine = await _context.OrdersLines.FindAsync(id);

            if (orderLine == null)
            {
                return NotFound();
            }

            return orderLine;
        }

        // PUT: api/OrderLine/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderLine(string id, OrdersLine orderLine)
        {
            if (id != orderLine.OrdersLineId)
            {
                return BadRequest();
            }

            _context.Entry(orderLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderLineExists(id))
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

        // POST: api/OrderLine
        [HttpPost]
        public async Task<ActionResult<OrdersLine>> PostOrderLine(OrdersLine orderLine)
        {
            _context.OrdersLines.Add(orderLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderLine", new { id = orderLine.OrdersLineId }, orderLine);
        }

        // DELETE: api/OrderLine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderLine(string id)
        {
            var orderLine = await _context.OrdersLines.FindAsync(id);
            if (orderLine == null)
            {
                return NotFound();
            }

            _context.OrdersLines.Remove(orderLine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderLineExists(string id)
        {
            return _context.OrdersLines.Any(e => e.OrdersLineId == id);
        }
    }
}

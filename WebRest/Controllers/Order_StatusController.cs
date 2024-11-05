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
    public class OrderStatusController : ControllerBase
    {
        private readonly WebRestOracleContext _context;

        public OrderStatusController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/Order_Status
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatus>>> GetOrder_Status()
        {
            return await _context.OrderStatuses.ToListAsync();
        }

        // GET: api/Order_Status/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderStatus>> GetOrder_Status(string id)
        {
            var orderStatus = await _context.OrderStatuses.FindAsync(id);

            if (orderStatus == null)
            {
                return NotFound();
            }

            return orderStatus;
        }

        // PUT: api/Order_Status/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder_Status(string id, OrderStatus orderStatus)
        {
            if (id != orderStatus.OrderStatusId)
            {
                return BadRequest();
            }

            _context.Entry(orderStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_StatusExists(id))
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

        // POST: api/Order_Status
        [HttpPost]
        public async Task<ActionResult<OrderStatus>> PostOrder_Status(OrderStatus orderStatus)
        {
            _context.OrderStatuses.Add(orderStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder_Status", new { id = orderStatus.OrderStatusId }, orderStatus);
        }

        // DELETE: api/Order_Status/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder_Status(string id)
        {
            var orderStatus = await _context.OrderStatuses.FindAsync(id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            _context.OrderStatuses.Remove(orderStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Order_StatusExists(string id)
        {
            return _context.OrderStatuses.Any(e => e.OrderStatusId == id);
        }
    }
}

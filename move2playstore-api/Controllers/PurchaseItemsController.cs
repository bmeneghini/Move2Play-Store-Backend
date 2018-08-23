using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using move2playstoreAPI.Models;

namespace move2playstoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseItemsController : ControllerBase
    {
        private readonly Move2PlayStoreDBContext _context;

        public PurchaseItemsController(Move2PlayStoreDBContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseItems
        [HttpGet]
        public IEnumerable<Purchaseitem> GetPurchaseitem()
        {
            return _context.Purchaseitem;
        }

        // GET: api/PurchaseItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseitem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purchaseitem = await _context.Purchaseitem.FindAsync(id);

            if (purchaseitem == null)
            {
                return NotFound();
            }

            return Ok(purchaseitem);
        }

        // PUT: api/PurchaseItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseitem([FromRoute] int id, [FromBody] Purchaseitem purchaseitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseitem.Id)
            {
                return BadRequest();
            }

            _context.Entry(purchaseitem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseitemExists(id))
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

        // POST: api/PurchaseItems
        [HttpPost]
        public async Task<IActionResult> PostPurchaseitem([FromBody] Purchaseitem purchaseitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Purchaseitem.Add(purchaseitem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseitem", new { id = purchaseitem.Id }, purchaseitem);
        }

        // DELETE: api/PurchaseItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseitem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purchaseitem = await _context.Purchaseitem.FindAsync(id);
            if (purchaseitem == null)
            {
                return NotFound();
            }

            _context.Purchaseitem.Remove(purchaseitem);
            await _context.SaveChangesAsync();

            return Ok(purchaseitem);
        }

        private bool PurchaseitemExists(int id)
        {
            return _context.Purchaseitem.Any(e => e.Id == id);
        }
    }
}
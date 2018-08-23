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
    public class UsergamesController : ControllerBase
    {
        private readonly Move2PlayStoreDBContext _context;

        public UsergamesController(Move2PlayStoreDBContext context)
        {
            _context = context;
        }

        // GET: api/Usergames
        [HttpGet]
        public IEnumerable<Usergame> GetUsergame()
        {
            return _context.Usergame;
        }

        // GET: api/Usergames/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsergame([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usergame = await _context.Usergame.FindAsync(id);

            if (usergame == null)
            {
                return NotFound();
            }

            return Ok(usergame);
        }

        // PUT: api/Usergames/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsergame([FromRoute] int id, [FromBody] Usergame usergame)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usergame.UserId)
            {
                return BadRequest();
            }

            _context.Entry(usergame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsergameExists(id))
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

        // POST: api/Usergames
        [HttpPost]
        public async Task<IActionResult> PostUsergame([FromBody] Usergame usergame)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Usergame.Add(usergame);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsergameExists(usergame.UserId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsergame", new { id = usergame.UserId }, usergame);
        }

        // DELETE: api/Usergames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsergame([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usergame = await _context.Usergame.FindAsync(id);
            if (usergame == null)
            {
                return NotFound();
            }

            _context.Usergame.Remove(usergame);
            await _context.SaveChangesAsync();

            return Ok(usergame);
        }

        private bool UsergameExists(int id)
        {
            return _context.Usergame.Any(e => e.UserId == id);
        }
    }
}
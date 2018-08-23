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
    public class RatingsController : ControllerBase
    {
        private readonly Move2PlayStoreDBContext _context;

        public RatingsController(Move2PlayStoreDBContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet]
        public IEnumerable<Rating> GetRating()
        {
            return _context.Rating;
        }

        // GET: api/Ratings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRating([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rating = await _context.Rating.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        // PUT: api/Ratings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating([FromRoute] int id, [FromBody] Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rating.Id)
            {
                return BadRequest();
            }

            _context.Entry(rating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
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

        // POST: api/Ratings
        [HttpPost]
        public async Task<IActionResult> PostRating([FromBody] Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Rating.Add(rating);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRating", new { id = rating.Id }, rating);
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rating = await _context.Rating.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Rating.Remove(rating);
            await _context.SaveChangesAsync();

            return Ok(rating);
        }

        private bool RatingExists(int id)
        {
            return _context.Rating.Any(e => e.Id == id);
        }
    }
}
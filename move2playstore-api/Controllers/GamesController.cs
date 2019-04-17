using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using move2playstoreAPI.Controllers.Mappers;
using move2playstoreAPI.DataTransferObjects;
using move2playstoreAPI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace move2playstoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly Move2PlayStoreDBContext _context;

        public GamesController(Move2PlayStoreDBContext context)
        {
            _context = context;
        }

        // GET: api/Games
        [HttpGet]
        public IActionResult GetGame()
        {
            try
            {
                var gamesList = _context.Game
                    .Include(game => game.Developer)
                    .Include(game => game.Image)
                    .Include(game => game.Video)
                    .Include(game => game.Rating)
                    .Include(game => game.Comment)
                    .Take(4);
                var gameDtoList = gamesList.Select(model => GameMapper.ConvertModelToDto(model)).ToList();
                return Ok(gameDtoList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGame([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var game = await _context.Game.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        // PUT: api/Games/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame([FromRoute] int id, [FromBody] Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != game.Id)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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

        // POST: api/Games
        [HttpPost]
        public async Task<IActionResult> PostGame([FromBody] GameUploadDto gameDto)
        {
            if (gameDto == null)
            {
                return BadRequest();
            }

            try
            {
                var game = GameMapper.ConvertDtoToModel(gameDto);

                _context.Game.Add(game);
                await _context.SaveChangesAsync();

                SaveGameTrailer(game.Id, gameDto.TrailerUrl);

                return Ok(game.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/Games/Filters
        [HttpPost("Filters")]
        public IActionResult GetGameWithFilters([FromBody] GameFilterDto filterDto)
        {
            if (filterDto == null)
            {
                return BadRequest();
            }
            try
            {
                var gamesList = _context.Game
                    .Include(game => game.Developer)
                    .Include(game => game.Image)
                    .Include(game => game.Video)
                    .Include(game => game.Rating)
                    .Include(game => game.Comment);
                var gameDtoList = gamesList.Select(model => GameMapper.ConvertModelToDto(model)).ToList();
                if (filterDto.Name.Length > 0)
                {
                    gameDtoList = gameDtoList.Where(game => game.Name.Contains(filterDto.Name)).ToList();
                }
                if (filterDto.Price > 0)
                {
                    gameDtoList = gameDtoList.Where(game => game.Price <= filterDto.Price).ToList();
                }
                if (filterDto.Genre.Length <= 0)
                {
                    return Ok(gameDtoList);
                }
                {
                    var genre = GameMapper.ConvertGenderFieldToEnum(filterDto.Genre);
                    gameDtoList = gameDtoList.Where(game => game.Genre.Contains(genre)).ToList();
                }
                return Ok(gameDtoList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Game.Remove(game);
            await _context.SaveChangesAsync();

            return Ok(game);
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.Id == id);
        }

        private void SaveGameTrailer(int gameId, string trailerPath)
        {
            var video = new Video()
            {
                GameId = gameId,
                Path = trailerPath
            };
            _context.Video.Add(video);
            _context.SaveChanges();
        }
    }
}
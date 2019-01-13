using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using move2playstoreAPI.Models;
using System;
using System.IO;

namespace move2playstoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly Move2PlayStoreDBContext _context;

        public FileController(Move2PlayStoreDBContext context)
        {
            _context = context;
        }

        [HttpPost("{gameId}")]
        public async System.Threading.Tasks.Task<ActionResult> UploadFileAsync(IFormFile file, [FromRoute] int gameId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest();
            }

            var fileType = GetFileTypePath(file.ContentType);

            var path = Path.Combine(
                        Directory.GetCurrentDirectory()
                        , "wwwroot"
                        , fileType
                        , file.FileName);

            if (CheckIfFileNameAlreadyExists(path))
            {
                path = Path.Combine(
                        Directory.GetCurrentDirectory()
                        , "wwwroot"
                        , fileType
                        , Guid.NewGuid().ToString() + file.FileName);
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            if (string.Equals(fileType, "logos"))
            {
                SaveImage(path, gameId);
            }
            else if (string.Equals(fileType, "games"))
            {
                UpdateGamePath(path, gameId);
            }

            return Ok();
        }

        private bool CheckIfFileNameAlreadyExists(string path)
        {
            return System.IO.File.Exists(path) ? true : false;
        }

        private string GetFileTypePath(string contentType)
        {
            switch (contentType)
            {
                case "image/jpeg":
                case "image/png":
                    return "logos";
                case "application/zip":
                case "application/x-7z-compressed":
                case "application/x-rar-compressed":
                default:
                    return "games";
            }
        }

        private void UpdateGamePath(string path, int gameId)
        {
            var game = _context.Game.Find(gameId);
            game.ServerPath = path;
            _context.SaveChanges();
        }

        private void SaveImage(string path, int gameId)
        {
            var image = new Image()
            {
                GameId = gameId,
                Path = path
            };
            _context.Image.Add(image);
            _context.SaveChanges();
        }
    }
}

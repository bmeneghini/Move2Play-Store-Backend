using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using move2playstoreAPI.Models;
using System;
using System.IO;
using System.Threading.Tasks;

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
        public async Task<ActionResult> UploadFileAsync(IFormFile file, [FromRoute] int gameId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest();
            }
            try
            {
                var gameFolder = SetupFolders(gameId);

                var fileType = GetFileTypePath(file.ContentType);

                var path = Path.Combine(gameFolder, fileType, file.FileName);

                if (CheckIfFileNameAlreadyExists(path))
                {
                    path = Path.Combine(gameFolder, fileType, Guid.NewGuid() + file.FileName);
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                if (string.Equals(fileType, "Imagens"))
                {
                    SaveImage(path, gameId);
                }
                else if (string.Equals(fileType, "Arquivo de Jogo"))
                {
                    UpdateGamePath(path, gameId);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private static bool CheckIfFileNameAlreadyExists(string path)
        {
            return System.IO.File.Exists(path);
        }

        private static string GetFileTypePath(string contentType)
        {
            switch (contentType)
            {
                case "image/jpeg":
                case "image/png":
                    return "Imagens";
                case "application/zip":
                case "application/x-7z-compressed":
                case "application/x-rar-compressed":
                    return "Arquivo de Jogo";
                default:
                    return "Arquivo de Jogo";
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

        private static string SetupFolders(int gameId)
        {
            var root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            var gameFolder = Path.Combine(root, gameId.ToString());
            if (!Directory.Exists(gameFolder))
            {
                Directory.CreateDirectory(gameFolder);
            }
            if (!Directory.Exists(Path.Combine(gameFolder, "Imagens")))
            {
                Directory.CreateDirectory(Path.Combine(gameFolder, "Imagens"));
            }
            if (!Directory.Exists(Path.Combine(gameFolder, "Arquivo de Jogo")))
            {
                Directory.CreateDirectory(Path.Combine(gameFolder, "Arquivo de Jogo"));
            }
            return gameFolder;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using move2playstoreAPI.Models;
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

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest();
            }

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return RedirectToAction("Files");

        }
    }
}

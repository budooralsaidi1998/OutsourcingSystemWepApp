using Microsoft.AspNetCore.Mvc;

namespace OutsourcingSystemWepApp.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FileController : ControllerBase
    {
        private readonly string _baseDirectory = @"C:\Users\Lenovo\source\repos\OutsourcingSystemWepApp\wwwroot\DeveloperCVs";

        [HttpGet("{fileName}")]
        public IActionResult DownloadFile(string fileName)
        {
            string filePath = Path.Combine(_baseDirectory, fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found.");

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }
    }
}

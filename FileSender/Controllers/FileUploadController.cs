using FileSender.EfModels;
using FileSender.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileSender.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;

        public FileUploadController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile ([FromForm] FileUpload file)
        {
            try
            {
                var result = await _fileUploadService.UploadFile(file).ConfigureAwait(false);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("getfile")]
        public async Task<IActionResult> GetFileByGuid(Guid guid)
        {
            try
            {
                var result = await _fileUploadService.GetFileByGuid(guid).ConfigureAwait(false);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }

        }


    }
}

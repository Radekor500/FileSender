using FileSender.DtoModels;
using FileSender.EfModels;
using FileSender.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Web;

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
        public async Task<IActionResult> UploadFile ([FromForm] FileUploadForm file)
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
                var result = await _fileUploadService.GetFilesByGuid(guid).ConfigureAwait(false);
                //var test = result.FileContent.FirstOrDefault();
                return File(test.FileData, _fileUploadService.GetContentType(test.FileName), test.FileName);
                //return Ok(result);
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }

        }


    }
}

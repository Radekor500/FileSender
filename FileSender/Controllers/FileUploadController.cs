using FileSender.Bindings;
using FileSender.DtoModels;
using FileSender.EfModels;
using FileSender.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.WebUtilities;
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

        [RequestSizeLimit(2000000000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 2000000000)]
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

        [HttpGet("downloadall")]
        public async Task<IActionResult> DownloadAllFilesByGuid(Guid guid)
        {
            try
            {
                var result = await _fileUploadService.DownloadAllFilesByGuid(guid).ConfigureAwait(false);
                return File(result, "application/zip", "archive.zip");
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("downloadsingle")]
        public async Task<IActionResult> DownloadSingleFileByGuid(Guid guid)
        {
            try
            {
                var result = await _fileUploadService.DownloadSingleFilesByGuid(guid).ConfigureAwait(false);
                return File(result.FileContent1, _fileUploadService.GetContentType(result.FileName), result.FileName);
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("listall")]
        public async Task<IActionResult> ListAllFilesByGuid(Guid guid)
        {
            try
            {
                var result = await _fileUploadService.ListAllFilesByGuid(guid).ConfigureAwait(false);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }

        }

        //[DisableFormValueModelBinding]
        //[RequestSizeLimit(2000000000)]
        //[RequestFormLimits(MultipartBodyLengthLimit = 2000000000)]
        [HttpPost("upload2")]
        public async Task<IActionResult> ReceiveFile(IFormFile file)
        {
           
            var reader = new MultipartReader("Content - Type: application / json; charset = utf - 8",Request.Body);

            // note: this is for a single file, you could also process multiple files
            var section = await reader.ReadNextSectionAsync();

           

            using var fileStream = section.Body;
            return Ok();
        }


    }
}

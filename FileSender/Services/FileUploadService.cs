using FileSender.DtoModels;
using FileSender.EfModels;
using FileSender.Repositories;
using Microsoft.AspNetCore.StaticFiles;

namespace FileSender.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IFileUploadRepository _fileUploadRepository;


        public FileUploadService(IFileUploadRepository fileUploadRepository)
        {
            _fileUploadRepository = fileUploadRepository;
        }

        //private FileUploadDto MapToDto(FileUpload file)
        //{
        //    return new FileUploadDto() { FileName = file.FileName, FileContent };
        //}

        public async Task<FileUpload> GetFileByGuid(Guid guid)
        {
           var result = await _fileUploadRepository.GetFileByGuid(guid).ConfigureAwait(false);
           return result;
        }

        public async Task<FileUpload> UploadFile(FileUploadDto file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.FileContent.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            var uploadFile = new FileUpload() { FileName = file.FileContent.FileName, FileContent = fileBytes, ExpiryDate = file.ExpiryDate };
            var result = await _fileUploadRepository.UploadFile(uploadFile).ConfigureAwait(false);
            return result;
        }

        public string GetContentType(string fileName)
        {
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
            return contentType ?? "application/octet-stream";
        }
    }
}

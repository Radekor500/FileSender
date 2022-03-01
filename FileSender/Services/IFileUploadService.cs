using FileSender.DtoModels;
using FileSender.EfModels;

namespace FileSender.Services
{
    public interface IFileUploadService
    {
        string GetContentType(string fileName);
        Task<FileUploadDto> GetFilesByGuid(Guid guid);
        Task<FileUploadDto> UploadFile(FileUploadForm file);
    }
}
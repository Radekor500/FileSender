using FileSender.DtoModels;
using FileSender.EfModels;

namespace FileSender.Services
{
    public interface IFileUploadService
    {
        Task<byte[]> DownloadAllFilesByGuid(Guid guid);
        Task<FileContent> DownloadSingleFilesByGuid(Guid guid);
        string GetContentType(string fileName);
        Task<IEnumerable<FileContentsDto>> ListAllFilesByGuid(Guid guid);
        Task<FileUploadDto> UploadFile(FileUploadForm file);
    }
}
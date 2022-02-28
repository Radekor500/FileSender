using FileSender.EfModels;

namespace FileSender.Services
{
    public interface IFileUploadService
    {
        Task<FileUpload> GetFileByGuid(Guid guid);
        Task<FileUpload> UploadFile(FileUpload file);
    }
}
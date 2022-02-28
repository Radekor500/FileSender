using FileSender.EfModels;

namespace FileSender.Repositories
{
    public interface IFileUploadRepository
    {
        Task<FileUpload> GetFileByGuid(Guid guid);
        Task<FileUpload> UploadFile(FileUpload file);

     
    }
}

using FileSender.EfModels;

namespace FileSender.Repositories
{
    public interface IFileContentsRepository
    {
        Task<IEnumerable<FileContent>> GetFilesContentsByGuidAsync(Guid guid);
        Task<FileContent> UploadFileContent(FileContent fileContent);
    }
}
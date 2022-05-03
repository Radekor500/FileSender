using FileSender.DtoModels;
using FileSender.EfModels;

namespace FileSender.Repositories
{
    public interface IFileContentsRepository
    {
        Task<IEnumerable<FileContent>> GetAllFilesContentsByGuidAsync(Guid guid);
        Task<FileContent> GetSingleFileContentsByGuid(Guid guid);
        Task<FileContent> UploadFileContent(FileContent fileContent);

        Task<IEnumerable<FileContentsDto>> GetAllFilesContentsNamesByGuidAsync(Guid guid);
    }
}
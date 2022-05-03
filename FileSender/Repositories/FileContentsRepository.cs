using FileSender.DtoModels;
using FileSender.EfModels;
using Microsoft.EntityFrameworkCore;

namespace FileSender.Repositories
{
    public class FileContentsRepository : IFileContentsRepository
    {
        private readonly SendDbContext _dbContext;

        public FileContentsRepository(SendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<FileContent>> GetAllFilesContentsByGuidAsync(Guid guid)
        {
            var files = await _dbContext.FileContents.Where(x => x.FileUploadId == guid).ToListAsync();
            if (files.Count == 0)
                throw new ArgumentException("File with provided guid does not exist");
            return files;
        }

        public async Task<IEnumerable<FileContentsDto>> GetAllFilesContentsNamesByGuidAsync(Guid guid)
        {
            var files = await _dbContext.FileContents.Where(x => x.FileUploadId == guid).Select(x => new {x.FileName, x.FileId}).ToListAsync();
            if (files.Count == 0)
                throw new ArgumentException("File with provided guid does not exist");
            return files.Select(x => new FileContentsDto() { FileName = x.FileName, FileId = x.FileId});
        }


        public async Task<FileContent> GetSingleFileContentsByGuid(Guid guid)
        {
            var file = await _dbContext.FileContents.Where(x => x.FileId == guid).FirstOrDefaultAsync();
            if (file == null)
                throw new ArgumentException("File with provided guid does not exist");
            return file;
        }

        public async Task<FileContent> UploadFileContent(FileContent fileContent)
        {
            var uploadFile = _dbContext.FileContents.AddAsync(fileContent).Result.Entity;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return uploadFile;
        }
    }
}

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

        public async Task<IEnumerable<FileContent>> GetFilesContentsByGuidAsync(Guid guid)
        {
            var files = await _dbContext.FileContents.Where(x => x.FileUploadId == guid).ToListAsync();
            if (files == null)
                throw new ArgumentException("File with provided guid does not exist");
            return files;
        }

        public async Task<FileContent> UploadFileContent(FileContent fileContent)
        {
            var uploadFile = _dbContext.FileContents.AddAsync(fileContent).Result.Entity;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return uploadFile;
        }
    }
}

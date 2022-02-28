using FileSender.EfModels;
using Microsoft.EntityFrameworkCore;

namespace FileSender.Repositories
{
    public class FileUploadRepository : IFileUploadRepository
    {
        private readonly SendDbContext _dbContext;

        public FileUploadRepository(SendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FileUpload> GetFileByGuid(Guid guid)
        {
            var file = await _dbContext.FileUploads.SingleOrDefaultAsync(file => file.Id == guid);
            if (file == null)
                throw new ArgumentException("File with provided guid does not exist");
            return file;
        }

        public async Task<FileUpload> UploadFile(FileUpload file)
        {
            var uploadFile = _dbContext.FileUploads.AddAsync(file).Result.Entity;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return uploadFile;
        }
    }
}

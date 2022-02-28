using FileSender.EfModels;
using FileSender.Repositories;

namespace FileSender.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IFileUploadRepository _fileUploadRepository;

        public FileUploadService(IFileUploadRepository fileUploadRepository)
        {
            _fileUploadRepository = fileUploadRepository;
        }

        public async Task<FileUpload> GetFileByGuid(Guid guid)
        {
           var result = await _fileUploadRepository.GetFileByGuid(guid).ConfigureAwait(false);
           return result;
        }

        public async Task<FileUpload> UploadFile(FileUpload file)
        {
            var result = await _fileUploadRepository.UploadFile(file).ConfigureAwait(false);
            return result;
        }
    }
}

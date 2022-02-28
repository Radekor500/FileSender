using FileSender.DtoModels;
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

        public async Task<FileUpload> UploadFile(FileUploadDto file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.FileContent.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            var uploadFile = new FileUpload() { FileName = file.FileName, FileContent = fileBytes };
            var result = await _fileUploadRepository.UploadFile(uploadFile).ConfigureAwait(false);
            return result;
        }
    }
}

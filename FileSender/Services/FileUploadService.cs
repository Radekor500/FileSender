using FileSender.DtoModels;
using FileSender.EfModels;
using FileSender.Repositories;
using Microsoft.AspNetCore.StaticFiles;
using System.IO.Compression;

namespace FileSender.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IFileUploadRepository _fileUploadRepository;
        private readonly IFileContentsRepository _fileContentsRepository;


        public FileUploadService(IFileUploadRepository fileUploadRepository,
                                 IFileContentsRepository fileContentsRepository)
        {
            _fileUploadRepository = fileUploadRepository;
            _fileContentsRepository = fileContentsRepository;
        }

        private IEnumerable<FileContentsDto> BuildFileList(IEnumerable<FileContent> fileContent)
        {
            var fileUploadDto = fileContent.Select(fileContent => new FileContentsDto()
            {
                FileName = fileContent.FileName,
                FileId = fileContent.FileId,
            });
            return fileUploadDto;

        }

        private byte[] CreateZip(IEnumerable<FileContent> files)
        {
            using (var stream = new MemoryStream())
            {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        var entry = archive.CreateEntry(file.FileName);
                        using (var zipStream = entry.Open())
                        {
                            zipStream.Write(file.FileContent1, 0, file.FileContent1.Length);
                        }
                    }
                }
                return stream.ToArray();
            }
        }

        //seperate endpoint to download all files as zip, download signle file, list files, change db to have unique id for file
        public async Task<Byte[]> DownloadAllFilesByGuid(Guid guid)
        {
            var result = await _fileContentsRepository.GetAllFilesContentsByGuidAsync(guid);
            return CreateZip(result);
        }

        public async Task<FileContent> DownloadSingleFilesByGuid(Guid guid)
        {
            var result = await _fileContentsRepository.GetSingleFileContentsByGuid(guid);
            return result;
        }

        public async Task<IEnumerable<FileContentsDto>> ListAllFilesByGuid(Guid guid)
        {
            var dateCheck = await _fileUploadRepository.GetFileByGuid(guid);
            if (dateCheck.ExpiryDate > DateTime.Now || dateCheck.ExpiryDate == null)
            {
                var result = await _fileContentsRepository.GetAllFilesContentsByGuidAsync(guid);
                return BuildFileList(result);
            }
            throw new ArgumentException("Files have already expired");
        }

        public async Task<FileUploadDto> UploadFile(FileUploadForm file)
        {
            List<FileContent> files = new List<FileContent>();

            var uploadFile = new FileUpload() { ExpiryDate = file.ExpiryDate };
            var result = await _fileUploadRepository.UploadFile(uploadFile).ConfigureAwait(false);

            using (var ms = new MemoryStream())
            {
                foreach (var item in file.FileContent)
                {
                    item.CopyTo(ms);
                    files.Add(new FileContent() { FileContent1 = ms.ToArray(), FileName = item.FileName, FileUploadId = result.Id });
                    ms.SetLength(0);
                }

            }

            foreach (var item in files)
            {
                await _fileContentsRepository.UploadFileContent(item);
            }

            return new FileUploadDto() { UploadId = result.Id, UploadDate = result.UploadDate, ExpiryDate = result.ExpiryDate };
        }

        public string GetContentType(string fileName)
        {
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
            return contentType ?? "application/octet-stream";
        }
    }
}

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

        //private FileUploadDto MapToDto(FileUpload file)
        //{
        //    return new FileUploadDto() { FileName = file.FileName, FileContent };
        //}

        private FileUploadDto BuildFileDto(IEnumerable<FileContent> fileContent, FileUpload fileUpload)
        {
            //var fileUpload = await _fileUploadRepository.GetFileByGuid(guid);
            //var fileContent = await _fileContentsRepository.GetFilesContentsByGuidAsync(guid);
            var fileContentDto = fileContent.Select(x => new FileContentsDto()
            {
                FileName = x.FileName,
                FileData = x.FileContent1
            });
            return new FileUploadDto()
            {
                FileContent = fileContentDto,
                ExpiryDate = fileUpload.ExpiryDate,
                UploadDate = fileUpload.UploadDate,
            };

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

        public async Task<byte[]> GetFilesByGuid(Guid guid)
        {
            var result = await _fileContentsRepository.GetFilesContentsByGuidAsync(guid);
            if (result.ToList().Count > 1)
                return CreateZip(result);
            return result.FirstOrDefault().FileContent1;
            //return BuildFileDto(result, await _fileUploadRepository.GetFileByGuid(guid));
        }

        public async Task<FileUploadDto> UploadFile(FileUploadForm file)
        {
            List<FileContent> files = new List<FileContent>();

            var uploadFile = new FileUpload() { FileContents = files, ExpiryDate = file.ExpiryDate };
            var result = await _fileUploadRepository.UploadFile(uploadFile).ConfigureAwait(false);

            using (var ms = new MemoryStream())
            {
                foreach (var item in file.FileContent)
                {
                    item.CopyTo(ms);
                    //files.Add(ms.ToArray());
                    //ms.SetLength(0);
                    files.Add(new FileContent() { FileContent1 = ms.ToArray(), FileName = item.FileName, FileUploadId = result.Id });
                    ms.SetLength(0);
                }

            }

            foreach (var item in files)
            {
                await _fileContentsRepository.UploadFileContent(item);
            }

            return BuildFileDto(await _fileContentsRepository.GetFilesContentsByGuidAsync(result.Id) ,result);
        }

        public string GetContentType(string fileName)
        {
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
            return contentType ?? "application/octet-stream";
        }
    }
}

using FileSender.DtoModels;
using FileSender.EfModels;
using FileSender.Repositories;
using FileSender.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSender.Test.ServiceUnitTests
{
    public class FileUploadServiceUnitTests
    {
        private DbContextOptions<SendDbContext> _contextOptions = new DbContextOptionsBuilder<SendDbContext>()
            .UseInMemoryDatabase(databaseName: "FileSenderTest").Options;

        private SendDbContext _dbContext;
        private FileUploadService _fileUploadService;

        [SetUp]
        public void SetUp()
        {
            _dbContext = new SendDbContext(_contextOptions);
            SeedDb();
            _fileUploadService = new FileUploadService(new FileUploadRepository(_dbContext), 
                new FileContentsRepository(_dbContext)); 
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.FileContents.RemoveRange(_dbContext.FileContents);
            _dbContext.FileUploads.RemoveRange(_dbContext.FileUploads);
            _dbContext.SaveChanges();
        }

        private void SeedDb()
        {
            var fileUploadGuid = Guid.NewGuid();
            var fileContent = new FileContent()
            {
                FileId = Guid.NewGuid(),
                FileName = "UnitTest",
                FileContent1 = new byte[15],
                FileUploadId = fileUploadGuid,
            };

            var fileUpload = new FileUpload()
            {
                Id = fileUploadGuid,
                UploadDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(1),

            };
            _dbContext.FileContents.Add(fileContent);
            _dbContext.FileUploads.Add(fileUpload);

            _dbContext.SaveChanges();
        }

        [Test]
        public async Task DownloadAllFilesByGuid_Returns_ByteArray()
        {
            var contextFile = _dbContext.FileContents.First();
            var files = await _fileUploadService.DownloadAllFilesByGuid(contextFile.FileUploadId);

            Assert.That(files != null && files is Byte[]);
        }

        [Test]
        public async Task DownloadSingleFileByGuid_Returns_FileContnet()
        {
            var contextFile = _dbContext.FileContents.First();
            var files = await _fileUploadService.DownloadSingleFilesByGuid(contextFile.FileId);

            Assert.That(files != null && files is FileContent);
        }

        [Test]
        public async Task ListAllFilesByGuid_Returns_FileContnet()
        {
            var contextFile = _dbContext.FileContents.First();
            var files = await _fileUploadService.ListAllFilesByGuid(contextFile.FileUploadId);

            Assert.That(files != null && files is IEnumerable<FileContentsDto>);
        }


        [Test]
        public async Task ListAllFilesByGuid_Throws_ArgumentException()
        {
            var fileUploadGuid = Guid.NewGuid();
            var fileContent = new FileContent()
            {
                FileId = Guid.NewGuid(),
                FileName = "ExpiryTest",
                FileContent1 = new byte[15],
                FileUploadId = fileUploadGuid,
            };

            var fileUpload = new FileUpload()
            {
                Id = fileUploadGuid,
                UploadDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(-7),

            };

            _dbContext.FileContents.Add(fileContent);
            _dbContext.FileUploads.Add(fileUpload);

            _dbContext.SaveChanges();

            Assert.ThrowsAsync<ArgumentException>(async () => await _fileUploadService.ListAllFilesByGuid(fileContent.FileUploadId));
        }
    }
}

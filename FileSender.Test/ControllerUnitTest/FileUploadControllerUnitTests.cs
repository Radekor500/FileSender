using FileSender.Controllers;
using FileSender.DtoModels;
using FileSender.EfModels;
using FileSender.Repositories;
using FileSender.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSender.Test.ControllerUnitTest
{
    public class FileUploadControllerUnitTests
    {
        private DbContextOptions<SendDbContext> _contextOptions = new DbContextOptionsBuilder<SendDbContext>()
            .UseInMemoryDatabase(databaseName: "FileSenderTest").Options;

        private SendDbContext _dbContext;
        private FileUploadService _fileUploadService;
        private FileUploadController _fileUploadController;

        [SetUp]
        public void SetUp()
        {
            _dbContext = new SendDbContext(_contextOptions);
            SeedDb();
            _fileUploadService = new FileUploadService(new FileUploadRepository(_dbContext),
                new FileContentsRepository(_dbContext));

            _fileUploadController = new FileUploadController(_fileUploadService);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
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
        public async Task DownloadAllFilesByGuid_Returns_OK()
        {
            var contextFile = _dbContext.FileContents.First();
            var response = await _fileUploadController.DownloadAllFilesByGuid(contextFile.FileUploadId);

            Assert.That(response is FileResult fileRes && fileRes.ContentType == "application/zip");
        }

        [Test]
        public async Task DownloadAllFilesByGuid_Returns_BadRequest()
        {
            var response = await _fileUploadController.DownloadAllFilesByGuid(Guid.NewGuid());

            Assert.That(response is BadRequestObjectResult);
        }

        [Test]
        public async Task DownloadSingleFileByGuid_Returns_OK()
        {
            var contextFile = _dbContext.FileContents.First();
            var response = await _fileUploadController.DownloadSingleFileByGuid(contextFile.FileId);

            Assert.That(response is FileResult);
        }

        [Test]
        public async Task DownloadSingleFileByGuid_Returns_BadRequest()
        {
            var response = await _fileUploadController.DownloadSingleFileByGuid(Guid.NewGuid());

            Assert.That(response is BadRequestObjectResult);
        }

        [Test]
        public async Task ListAllFileseByGuid_Returns_OK()
        {
            var contextFile = _dbContext.FileContents.First();
            var response = await _fileUploadController.ListAllFilesByGuid(contextFile.FileUploadId);

            Assert.That(response is OkObjectResult okResult && okResult.Value is IEnumerable<FileContentsDto> files && files.ToList().Count > 0);
        }

        [Test]
        public async Task ListAllFilesByGuid_Returns_BadRequest()
        {
            var response = await _fileUploadController.ListAllFilesByGuid(Guid.NewGuid());

            Assert.That(response is BadRequestObjectResult);
        }


    }
}

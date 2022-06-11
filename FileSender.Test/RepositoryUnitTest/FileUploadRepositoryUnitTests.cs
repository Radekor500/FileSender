using FileSender.EfModels;
using FileSender.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSender.Test.RepositoryUnitTest
{
    public class FileUploadRepositoryUnitTests
    {
        private DbContextOptions<SendDbContext> _contextOptions = new DbContextOptionsBuilder<SendDbContext>()
            .UseInMemoryDatabase(databaseName: "FileSenderTest").Options;

        private SendDbContext _dbContext;
        private FileUploadRepository _fileUploadRepository;

        [SetUp]
        public void SetUp()
        {
            _dbContext = new SendDbContext(_contextOptions);
            SeedDb();
            _fileUploadRepository = new FileUploadRepository(_dbContext);
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
        public async Task GetFileByGuid_Returns_FileUpload()
        {
            var contextFile = _dbContext.FileUploads.First();
            var file = await _fileUploadRepository.GetFileByGuid(contextFile.Id);

            Assert.That(file != null && file is FileUpload);
        }

        [Test]
        public async Task GetFileByGuid_Throws_ArgumentException()
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await _fileUploadRepository.GetFileByGuid(Guid.NewGuid()));
        }

        [Test]
        public async Task UploadFile_Returns_FileUpload()
        {
            var id = Guid.NewGuid();

            var fileUpload = new FileUpload()
            {
                Id = id,
                UploadDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(1),

            };

            var uploadedFile = await _fileUploadRepository.UploadFile(fileUpload);
            Assert.That(uploadedFile != null && uploadedFile.Id == id);
        }
    }
}

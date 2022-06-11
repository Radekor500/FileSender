using FileSender.DtoModels;
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
    public class FileContentsRepositoryUnitTests
    {
        private DbContextOptions<SendDbContext> _contextOptions = new DbContextOptionsBuilder<SendDbContext>()
            .UseInMemoryDatabase(databaseName: "FileSenderTest").Options;

        private SendDbContext _dbContext;
        private FileContentsRepository _fileContentsRepository;

        [SetUp]
        public void SetUp()
        {
            _dbContext = new SendDbContext(_contextOptions);
            SeedDb();
            _fileContentsRepository = new FileContentsRepository(_dbContext);
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
        public async Task GetAllFilesContentsByGuidAsync_Returns_Collection_Of_FileContent()
        {
            var contextFile = _dbContext.FileContents.First();
            var files = await _fileContentsRepository.GetAllFilesContentsByGuidAsync(contextFile.FileUploadId);
            
            Assert.That(files.Any() && files is IEnumerable<FileContent>);
        }

        [Test]
        public async Task GetAllFilesContentsByGuidAsync_Throws_ArgumentException()
        {

            Assert.ThrowsAsync<ArgumentException>(async () => 
            await _fileContentsRepository.GetAllFilesContentsByGuidAsync(Guid.NewGuid()));
        }

        [Test]
        public async Task GetAllFilesContentsNamesByGuidAsync_Returns_Collection_Of_FileContentDto()
        {
            var contextFile = _dbContext.FileContents.First();
            var files = await _fileContentsRepository.GetAllFilesContentsNamesByGuidAsync(contextFile.FileUploadId);

            Assert.That(files.Any() && files is IEnumerable<FileContentsDto>);
        }

        [Test]
        public async Task GetAllFilesContentsNamesByGuidAsync_Throws_ArgumentException()
        {

            Assert.ThrowsAsync<ArgumentException>(async () =>
            await _fileContentsRepository.GetAllFilesContentsNamesByGuidAsync(Guid.NewGuid()));
        }

        [Test]
        public async Task GetSingleFileContentsByGuiidAsync_Returns_FileContent()
        {
            var contextFile = _dbContext.FileContents.First();
            var files = await _fileContentsRepository.GetSingleFileContentsByGuid(contextFile.FileId);

            Assert.That(files != null && files is FileContent);
        }

        [Test]
        public async Task UploadFileContent_Returns_FileContent()
        {
            var id = Guid.NewGuid();
            var name = "UploadTest";


            var fileContent = new FileContent()
            {
                FileId = id,
                FileName = name,
                FileContent1 = new byte[15],
            };

            var uploadedFile = await _fileContentsRepository.UploadFileContent(fileContent);

            Assert.That(uploadedFile != null && uploadedFile.FileId == id && uploadedFile.FileName == name);
        }

    }
}

namespace FileSender.DtoModels
{
    public class FileUploadDto
    {
        public IEnumerable<FileContentsDto> FileContent { get; set; }
        public DateTime? UploadDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }

    public class FileUploadForm
    {
        public IEnumerable<IFormFile> FileContent { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}

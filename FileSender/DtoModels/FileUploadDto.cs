namespace FileSender.DtoModels
{
    public class FileContentsDto
    {
        //public IEnumerable<FileContentsDto> FileContent { get; set; }
        public string FileName { get; set; }
        public Guid FileId { get; set; }
        public DateTime? UploadDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }

    public class FileUploadDto
    {
        public Guid UploadId { get; set; }
        public DateTime? UploadDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }

    public class FileUploadForm
    {
        public IEnumerable<IFormFile> FileContent { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}

namespace FileSender.DtoModels
{
    public class FileUploadForm
    {
        public IEnumerable<IFormFile> FileContent { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}

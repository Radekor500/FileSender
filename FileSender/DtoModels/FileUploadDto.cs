namespace FileSender.DtoModels
{
    public class FileUploadDto
    {
        public string FileName { get; set; }
        public IFormFile FileContent { get; set; }
    }
}

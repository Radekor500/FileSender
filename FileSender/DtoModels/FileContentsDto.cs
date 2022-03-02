namespace FileSender.DtoModels
{
    public class FileContentsDto
    {
        public string FileName { get; set; }
        public Guid FileId { get; set; }
        public DateTime? UploadDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}

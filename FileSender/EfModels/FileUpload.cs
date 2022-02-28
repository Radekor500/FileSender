using System;
using System.Collections.Generic;

namespace FileSender.EfModels
{
    public partial class FileUpload
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = null!;
        public byte[] FileContent { get; set; } = null!;
        public DateTime UploadDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool? IsViewed { get; set; }
    }
}

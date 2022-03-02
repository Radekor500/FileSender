using System;
using System.Collections.Generic;

namespace FileSender.EfModels
{
    public partial class FileUpload
    {
        public Guid Id { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool? IsViewed { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace FileSender.EfModels
{
    public partial class FileUpload
    {
        public FileUpload()
        {
            FileContents = new HashSet<FileContent>();
        }

        public Guid Id { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool? IsViewed { get; set; }

        public virtual ICollection<FileContent> FileContents { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace FileSender.EfModels
{
    public partial class FileContent
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public byte[] FileContent1 { get; set; } = null!;
        public Guid? FileUploadId { get; set; }

        public virtual FileUpload? FileUpload { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Common.Enumerations;

namespace VinylExchange.Data.Models
{
    public class ReleaseFile
    {
        public Guid Id { get; set; }
        public string Path { get; set; }

        public string FileName { get; set; }

        public FileType FileType { get; set; }

        public DateTime CreatedOn { get; set; }

        public Release Release { get; set; }

        public Guid ReleaseId { get; set; }

    }
}

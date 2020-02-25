using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VinylExchange.Data.Common.Models;

namespace VinylExchange.Data.Models
{
    public class Release : IAuditInfo, IDeletableEntity
    {
        public Release()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public string Title { get; set; }             
        [Required]
        public string Format { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public string Label { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<StyleRelease> Styles { get; set; } = new HashSet<StyleRelease>();

        public ICollection<ReleaseFile> ReleaseFiles { get; set; } = new HashSet<ReleaseFile>();

        public ICollection<CollectionItem> ReleaseCollections { get; set; } = new HashSet<CollectionItem>();

        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();
  
    }
}

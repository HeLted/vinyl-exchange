using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VinylExchange.Data.Models
{
    public class Release
    {
        public Release()
        {
            this.Id = Guid.NewGuid();
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
        public ICollection<StyleRelease> Styles { get; set; } = new HashSet<StyleRelease>();

        public ICollection<ReleaseFile> ReleaseFiles { get; set; } = new HashSet<ReleaseFile>();

        public ICollection<CollectionItem> ReleaseCollections { get; set; } = new HashSet<CollectionItem>();

    }
}

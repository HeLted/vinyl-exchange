using System;
using System.ComponentModel.DataAnnotations;
using VinylExchange.Data.Common.Enumerations;

namespace VinylExchange.Data.Models
{
    public class CollectionItem
    {

        public CollectionItem()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        [Required]
        public Condition VinylGrade { get; set; }
        [Required]
        public Condition SleeveGrade{ get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public Guid ReleaseId { get; set; }
        public Release Release { get; set; }

        [Required]
        public string UserId { get; set; }
        public VinylExchangeUser User { get; set; }

    }
}

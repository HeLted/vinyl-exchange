using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VinylExchange.Data.Models.Enums;

namespace VinylExchange.Data.Models
{
    public class CollectionItem
    {

        public CollectionItem()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Condition Condition { get; set; }

        public string Description { get; set; }

        [Required]
        public Guid ReleaseId { get; set; }

        public Release Release { get; set; }

        [Required]
        public string UserId { get; set; }

        public VinylExchangeUser User { get; set; }



    }
}

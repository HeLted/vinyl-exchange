namespace VinylExchange.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using Microsoft.AspNetCore.Identity;
    using VinylExchange.Common.Constants;

    public class VinylExchangeUser : IdentityUser<Guid>, IAuditInfo, IDeletableEntity
    {
        public VinylExchangeUser()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
            Avatar = ImageConstants.DefaultUserAvatarImage;
        }

        public ICollection<Address> Addresses { get; set; } = new HashSet<Address>();

        [Required] [MaxLength(10000000)] public byte[] Avatar { get; set; }

        public ICollection<CollectionItem> Collection { get; set; } = new HashSet<CollectionItem>();

        public ICollection<SaleMessage> Messages { get; set; } = new HashSet<SaleMessage>();

        public ICollection<Sale> Purchases { get; set; } = new HashSet<Sale>();

        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }
    }
}
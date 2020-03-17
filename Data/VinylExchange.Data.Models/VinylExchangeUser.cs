namespace VinylExchange.Data.Models
{
    #region

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    using VinylExchange.Common.Constants;
    using VinylExchange.Data.Common.Models;

    #endregion

    public class VinylExchangeUser : IdentityUser<Guid>, IAuditInfo, IDeletableEntity
    {
        public VinylExchangeUser()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.UtcNow;
            this.Avatar = ImageConstants.DefaultUserAvatarImage;
        }

        public ICollection<Address> Addresses { get; set; } = new HashSet<Address>();

        [Required]
        [MaxLength(10000000)]
        public byte[] Avatar { get; set; }

        public ICollection<CollectionItem> Collection { get; set; } = new HashSet<CollectionItem>();

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public ICollection<SaleMessage> Messages { get; set; } = new HashSet<SaleMessage>();

        public DateTime? ModifiedOn { get; set; }

        public ICollection<Sale> Purchases { get; set; } = new HashSet<Sale>();

        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();
    }
}
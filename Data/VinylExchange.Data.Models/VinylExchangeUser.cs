using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VinylExchange.Common.Constants;
using VinylExchange.Data.Common.Models;

namespace VinylExchange.Data.Models
{
    public class VinylExchangeUser : IdentityUser<Guid>, IAuditInfo, IDeletableEntity
    {

        public VinylExchangeUser()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.UtcNow;
            this.Avatar = ImageConstants.DefaultUserAvatarImage;
                        
        }

        [Required]
        [MaxLength(10000000)]
        public byte[] Avatar { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

     
        public ICollection<CollectionItem> Collection { get; set; } = new HashSet<CollectionItem>();

        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();

        public ICollection<Sale> Purchases { get; set; } = new HashSet<Sale>();

        public ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
    }
}

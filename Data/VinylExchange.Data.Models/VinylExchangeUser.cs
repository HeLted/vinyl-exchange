using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using VinylExchange.Data.Common.Models;

namespace VinylExchange.Data.Models
{
    public class VinylExchangeUser : IdentityUser<Guid>, IAuditInfo, IDeletableEntity
    {

        public VinylExchangeUser()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.UtcNow;
            this.Roles = new HashSet<IdentityUserRole<Guid>>();
            this.Claims = new HashSet<IdentityUserClaim<Guid>>();
            this.Logins = new HashSet<IdentityUserLogin<Guid>>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<Guid>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; set; }

        public ICollection<CollectionItem> Collection { get; set; } = new HashSet<CollectionItem>();

        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();

        public ICollection<Sale> Purchases { get; set; } = new HashSet<Sale>();
    }
}

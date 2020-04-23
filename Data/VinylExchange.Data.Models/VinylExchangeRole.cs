namespace VinylExchange.Data.Models
{
    using System;
    using Common.Models;
    using Microsoft.AspNetCore.Identity;

    public class VinylExchangeRole : IdentityRole<Guid>, IAuditInfo, IDeletableEntity
    {
        public VinylExchangeRole()
            : this(null)
        {
        }

        public VinylExchangeRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
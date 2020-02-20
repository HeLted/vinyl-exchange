using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data.Common.Models;

namespace VinylExchange.Data.Models
{
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

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

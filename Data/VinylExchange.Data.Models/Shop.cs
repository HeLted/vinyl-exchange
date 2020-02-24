using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VinylExchange.Data.Common.Enumerations;
using VinylExchange.Data.Common.Models;

namespace VinylExchange.Data.Models
{
    public class Shop : IAuditInfo, IDeletableEntity
    {
        public Shop()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        [Required]
        public ShopType ShopType { get; set; }
        public string WebAddress { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<ShopFile> ShopFiles { get; set; } = new HashSet<ShopFile>();

        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();
    }
}

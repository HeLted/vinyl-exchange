using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VinylExchange.Data.Common.Enumerations;
using VinylExchange.Data.Common.Models;

namespace VinylExchange.Data.Models
{
    public class Sale : IAuditInfo, IDeletableEntity
    {
        public Sale()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.UtcNow;
            this.Status = Status.Open;

        }
        public Guid Id { get; set; }
                
        public Nullable<Guid>  SellerId { get; set; }

        public VinylExchangeUser Seller { get; set; }
              
        public Nullable<Guid> BuyerId { get; set; }
        public VinylExchangeUser Buyer { get; set; }
     
        public Shop Shop { get; set; }

        public Nullable<Guid> ShopId { get; set; }

        public Nullable<Guid> ReleaseId { get; set; }

        public Release Release { get; set; }

        [Required]
        public Status Status { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        [Required]
        public Condition VinylCondition { get; set; }
        [Required]
        public Condition SleeveCondition { get; set; }
        [Required]
        public string Description { get; set; }
        
        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

    }
}

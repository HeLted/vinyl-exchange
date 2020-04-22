namespace VinylExchange.Data.Models
{
    #region

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Common.Models;
   

    #endregion

    public class Sale : BaseDeletableModel 
    {
        [Required]
        [Range((int)Condition.Poor, (int)Condition.Mint)]
        public Condition VinylGrade { get; set; }

        [Required]
        [Range((int)Condition.Poor, (int)Condition.Mint)]
        public Condition SleeveGrade { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(400)]
        public string Description { get; set; }

        public VinylExchangeUser Buyer { get; set; }

        public Guid? BuyerId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [Required]
        [Range((int)Status.Open, (int)Status.Finished)]
        public Status Status { get; set; }

        public Release Release { get; set; }

        public Guid? ReleaseId { get; set; }

        public VinylExchangeUser Seller { get; set; }

        [Required]
        public Guid? SellerId { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal ShippingPrice { get; set; }

        [Required]
        public string ShipsFrom { get; set; }

        public string ShipsTo { get; set; }

        public string OrderId { get; set; }

        public ICollection<SaleLog> Logs { get; set; } = new HashSet<SaleLog>();

        public ICollection<SaleMessage> Messages { get; set; } = new HashSet<SaleMessage>();
    }
}
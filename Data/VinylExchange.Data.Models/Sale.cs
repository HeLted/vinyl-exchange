namespace VinylExchange.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Common.Models;

    public class Sale : BaseDeletableModel
    {
        public VinylExchangeUser Buyer { get; set; }

        public Guid? BuyerId { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<SaleLog> Logs { get; set; } = new HashSet<SaleLog>();

        public ICollection<SaleMessage> Messages { get; set; } = new HashSet<SaleMessage>();

        public string OrderId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public Release Release { get; set; }

        public Guid? ReleaseId { get; set; }

        public VinylExchangeUser Seller { get; set; }

        public Guid? SellerId { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal ShippingPrice { get; set; }

        public string ShipsFrom { get; set; }

        public string ShipsTo { get; set; }

        [Required]
        public Condition SleeveGrade { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public Condition VinylGrade { get; set; }
    }
}
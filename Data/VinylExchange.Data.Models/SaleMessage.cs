namespace VinylExchange.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Common.Models;

    public class SaleMessage : BaseAuditModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(150)]
        public string Content { get; set; }

        public Sale Sale { get; set; }

        public Guid SaleId { get; set; }

        public VinylExchangeUser User { get; set; }

        public Guid UserId { get; set; }
    }
}
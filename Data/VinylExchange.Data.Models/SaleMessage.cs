namespace VinylExchange.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class SaleMessage : BaseAuditModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(150)]
        public string Content { get; set; }

        public Sale Sale { get; set; }

        [Required] public Guid? SaleId { get; set; }

        public VinylExchangeUser User { get; set; }

        [Required] public Guid? UserId { get; set; }
    }
}
namespace VinylExchange.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class SaleLog : BaseAuditModel
    {
        [Required]
        public string Content { get; set; }

        public Sale Sale { get; set; }

        [Required]
        public Guid? SaleId { get; set; }
    }
}
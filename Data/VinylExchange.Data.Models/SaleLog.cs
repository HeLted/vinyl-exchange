﻿namespace VinylExchange.Data.Models
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Common.Models;

    #endregion

    public class SaleLog : BaseAuditModel
    {
        [Required]
        public string Content { get; set; }

        public Sale Sale { get; set; }

        public Guid SaleId { get; set; }
    }
}
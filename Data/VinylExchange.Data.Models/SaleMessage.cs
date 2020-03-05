using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VinylExchange.Data.Common.Models;

namespace VinylExchange.Data.Models
{
    public class SaleMessage : IAuditInfo
    {

        public SaleMessage()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.UtcNow;
        }

        public Guid Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(150)]
        public string Content { get; set; }

        public Guid SaleId { get; set; }

        public Sale Sale { get; set; }

        public Guid UserId { get; set; }

        public VinylExchangeUser User { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}

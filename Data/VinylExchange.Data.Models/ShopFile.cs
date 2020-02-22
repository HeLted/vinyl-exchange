using System;
using System.ComponentModel.DataAnnotations;
using VinylExchange.Data.Common.Models;

namespace VinylExchange.Data.Models
{
    public class ShopFile : BaseFile
    {
        public ShopFile()
        {
            this.Id = Guid.NewGuid();
        }

        public Shop Shop { get; set; }

        [Required]
        public Guid ShopId { get; set; }
    }
}

namespace VinylExchange.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Common.Models;

    public class Address : BaseModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Country { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        public string FullAddress { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string PostalCode { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Town { get; set; }

        public VinylExchangeUser User { get; set; }

        public Guid UserId { get; set; }
    }
}
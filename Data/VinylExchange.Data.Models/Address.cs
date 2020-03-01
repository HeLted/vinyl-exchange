using System;
using System.ComponentModel.DataAnnotations;

namespace VinylExchange.Data.Models
{
    public class Address
    {
        public Address()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Country { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Town { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string PostalCode { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        public string FullAddress { get; set; }
        public VinylExchangeUser User { get; set; }
        public Guid UserId { get; set; }
    }
}

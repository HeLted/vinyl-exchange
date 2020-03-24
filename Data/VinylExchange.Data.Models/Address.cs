namespace VinylExchange.Data.Models
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Common.Models;

    #endregion

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
      
        [Required]
        public Guid UserId { get; set; }
    }
}
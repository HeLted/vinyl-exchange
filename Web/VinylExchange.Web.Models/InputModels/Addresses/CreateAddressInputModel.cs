namespace VinylExchange.Web.Models.InputModels.Addresses
{
    #region

    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class CreateAddressInputModel : IMapTo<Address>
    {
        [Required]
        [MinLength(3, ErrorMessage = "Invalid min length of field!")]
        [MaxLength(40, ErrorMessage = "Invalid max length of field!")]

        public string Country { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Invalid min length of field!")]
        [MaxLength(300, ErrorMessage = "Invalid max length of field!")]

        public string FullAddress { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Invalid min length of field!")]
        [MaxLength(40, ErrorMessage = "Invalid max length of field!")]

        public string PostalCode { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Invalid min length of field!")]
        [MaxLength(40, ErrorMessage = "Invalid max length of field!")]

        public string Town { get; set; }
    }
}
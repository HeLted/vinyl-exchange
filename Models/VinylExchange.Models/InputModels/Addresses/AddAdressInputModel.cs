namespace VinylExchange.Models.InputModels.Addresses
{
    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    public class AddAdressInputModel : IMapTo<Address>
    {
        [Required]
        [MinLength(3, ErrorMessage = "Min length of field is 3")]
        [MaxLength(40, ErrorMessage = "Max length of field is 40")]

        public string Country { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Min length of field is 3")]
        [MaxLength(300, ErrorMessage = "Max length of field is 300")]

        public string FullAddress { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Min length of field is 3")]
        [MaxLength(40, ErrorMessage = "Max length of field is 40")]

        public string PostalCode { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Min length of field is 3")]
        [MaxLength(40, ErrorMessage = "Max length of field is 40")]

        public string Town { get; set; }
    }
}
namespace VinylExchange.Web.Models.InputModels.SaleMessages
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SendMessageInputModel
    {
        [Required] public Guid? SaleId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(150)]
        public string MessageContent { get; set; }
    }
}
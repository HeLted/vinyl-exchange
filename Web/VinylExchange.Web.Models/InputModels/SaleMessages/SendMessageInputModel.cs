using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VinylExchange.Web.Models.InputModels.SaleMessages
{
    public class SendMessageInputModel
    {
        [Required]
        public Guid? SaleId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(150)]
        public string MessageContent { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace VinylExchange.Web.Models.InputModels.Files
{
    public class RemoveFileInputModel
    {
        [Required]
        public Guid? Id { get; set; }

        [Required]
        public Guid FormSessionId { get; set; }
    }
}

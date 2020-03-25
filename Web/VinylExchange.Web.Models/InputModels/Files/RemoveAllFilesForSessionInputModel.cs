using System;
using System.ComponentModel.DataAnnotations;

namespace VinylExchange.Web.Models.InputModels.Files
{
    public class RemoveAllFilesForSessionInputModel
    {
        [Required]
        public Guid? FormSessionId { get; set; }

    }
}

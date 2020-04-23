namespace VinylExchange.Web.Models.InputModels.Files
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RemoveAllFilesForSessionInputModel
    {
        [Required] public Guid? FormSessionId { get; set; }
    }
}
namespace VinylExchange.Web.Models.InputModels.Files
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RemoveFileInputModel
    {
        [Required] public Guid? Id { get; set; }

        [Required] public Guid FormSessionId { get; set; }
    }
}
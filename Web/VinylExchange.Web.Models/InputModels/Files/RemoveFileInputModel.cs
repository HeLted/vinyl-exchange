namespace VinylExchange.Web.Models.InputModels.Files
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class RemoveFileInputModel
    {
        [Required]
        public Guid? Id { get; set; }

        [Required]
        public Guid FormSessionId { get; set; }
    }
}
namespace VinylExchange.Web.Models.InputModels.Files
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class RemoveAllFilesForSessionInputModel
    {
        [Required]
        public Guid? FormSessionId { get; set; }
    }
}
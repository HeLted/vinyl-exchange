namespace VinylExchange.Web.Models.ResourceModels.File
{
    using System;

    public class DeleteFileResourceModel
    {
        public Guid FileId { get; set; }

        public string FileName { get; set; }
    }
}
﻿namespace VinylExchange.Web.Models.ResourceModels.File
{
    #region

    using System;

    #endregion

    public class DeleteFileResourceModel
    {
        public Guid FileId { get; set; }

        public string FileName { get; set; }
    }
}
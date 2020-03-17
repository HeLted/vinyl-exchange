﻿namespace VinylExchange.Web.Models.ResourceModels.Sales
{
    #region

    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class CreateSaleResourceModel : IMapFrom<Sale>
    {
        public Guid Id { get; set; }
    }
}
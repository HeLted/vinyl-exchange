﻿using System;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Web.Models.ResourceModels.Sales
{
    public class RemoveSaleResourceModel : IMapFrom<Sale>
    {
        public Guid Id { get; set; }
    }
}

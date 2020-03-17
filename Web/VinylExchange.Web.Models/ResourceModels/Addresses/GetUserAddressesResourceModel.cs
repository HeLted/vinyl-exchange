﻿namespace VinylExchange.Web.Models.ResourceModels.Addresses
{
    #region

    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class GetUserAddressesResourceModel : IMapFrom<Address>
    {
        public string Country { get; set; }

        public string FullAddress { get; set; }

        public Guid Id { get; set; }

        public string PostalCode { get; set; }

        public string Town { get; set; }
    }
}
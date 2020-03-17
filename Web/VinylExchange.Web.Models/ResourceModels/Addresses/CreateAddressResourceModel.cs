namespace VinylExchange.Web.Models.ResourceModels.Addresses
{
    #region

    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class CreateAddressResourceModel : IMapFrom<Address>
    {
        public Guid Id { get; set; }
    }
}
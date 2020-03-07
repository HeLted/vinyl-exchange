namespace VinylExchange.Models.ResourceModels.Addresses
{
    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    public class RemoveAddressResourceModel : IMapFrom<Address>
    {
        public Guid Id { get; set; }
    }
}
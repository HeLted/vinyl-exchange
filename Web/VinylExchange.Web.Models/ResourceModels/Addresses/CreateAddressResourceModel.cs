namespace VinylExchange.Web.Models.ResourceModels.Addresses
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class CreateAddressResourceModel : IMapFrom<Address>
    {
        public Guid Id { get; set; }
    }
}
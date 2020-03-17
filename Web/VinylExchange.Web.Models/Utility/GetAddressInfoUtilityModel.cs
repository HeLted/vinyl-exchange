namespace VinylExchange.Web.Models.Utility
{
    #region

    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class GetAddressInfoUtilityModel : IMapFrom<Address>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}
namespace VinylExchange.Web.Models.ResourceModels.SaleMessages
{
    #region

    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class GetMessagesForSaleResourceModel : IMapFrom<SaleMessage>
    {
        public string Content { get; set; }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}
namespace VinylExchange.Web.Models.ResourceModels.SaleMessages
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class GetMessagesForSaleResourceModel : IMapFrom<SaleMessage>
    {
        public string Content { get; set; }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}
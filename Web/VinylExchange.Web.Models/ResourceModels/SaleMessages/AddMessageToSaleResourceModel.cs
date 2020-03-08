namespace VinylExchange.Web.Models.ResourceModels.SaleMessages
{
    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    public class AddMessageToSaleResourceModel : IMapFrom<SaleMessage>
    {
        public Guid Id { get; set; }

        public string Content { get; set; }
               
        public Guid UserId { get; set; }
    }
}
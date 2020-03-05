using System;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ResourceModels.SaleMessages
{
    public class GetMessagesForSaleResourceModel:IMapFrom<SaleMessage>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Content { get; set; }
    }
}

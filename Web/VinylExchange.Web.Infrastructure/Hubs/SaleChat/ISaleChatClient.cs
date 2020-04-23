namespace VinylExchange.Web.Infrastructure.Hubs.SaleChat
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.ResourceModels.SaleMessages;

    public interface ISaleChatClient
    {
        Task LoadMessageHistory(IEnumerable<GetMessagesForSaleResourceModel> messages);

        Task NewMessage(AddMessageToSaleResourceModel message);
    }
}
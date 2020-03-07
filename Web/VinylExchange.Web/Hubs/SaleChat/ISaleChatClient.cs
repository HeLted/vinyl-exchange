namespace VinylExchange.Web.Hubs.SaleChat
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Web.Models.ResourceModels.SaleMessages;

    public interface ISaleChatClient
    {
        Task LoadMessageHistory(IEnumerable<GetMessagesForSaleResourceModel> messages);

        Task NewMessage(AddMessageToSaleResourceModel message);
    }
}
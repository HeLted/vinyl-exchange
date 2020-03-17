namespace VinylExchange.Web.Hubs.SaleChat
{
    #region

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Web.Models.ResourceModels.SaleMessages;

    #endregion

    public interface ISaleChatClient
    {
        Task LoadMessageHistory(IEnumerable<GetMessagesForSaleResourceModel> messages);

        Task NewMessage(AddMessageToSaleResourceModel message);
    }
}
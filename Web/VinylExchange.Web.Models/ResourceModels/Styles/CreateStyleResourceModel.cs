namespace VinylExchange.Web.Models.ResourceModels.Styles
{
    #region

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class CreateStyleResourceModel : IMapFrom<Style>
    {
        public int Id { get; set; }
    }
}
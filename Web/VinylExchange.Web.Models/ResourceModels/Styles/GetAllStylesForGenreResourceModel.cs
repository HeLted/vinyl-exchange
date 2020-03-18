namespace VinylExchange.Web.Models.ResourceModels.Styles
{
    #region

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class GetAllStylesForGenreResourceModel : IMapFrom<Style>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
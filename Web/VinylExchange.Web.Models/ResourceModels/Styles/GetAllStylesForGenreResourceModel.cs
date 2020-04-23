namespace VinylExchange.Web.Models.ResourceModels.Styles
{
    using Data.Models;
    using Services.Mapping;

    public class GetAllStylesForGenreResourceModel : IMapFrom<Style>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
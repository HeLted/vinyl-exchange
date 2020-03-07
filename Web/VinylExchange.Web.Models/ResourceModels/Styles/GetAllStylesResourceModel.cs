namespace VinylExchange.Web.Models.ResourceModels.Styles
{
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    public class GetAllStylesResourceModel : IMapFrom<Style>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
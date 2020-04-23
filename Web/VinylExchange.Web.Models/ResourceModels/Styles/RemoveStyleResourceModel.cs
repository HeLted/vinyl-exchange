namespace VinylExchange.Web.Models.ResourceModels.Styles
{
    using Data.Models;
    using Services.Mapping;

    public class RemoveStyleResourceModel : IMapFrom<Style>
    {
        public int Id { get; set; }
    }
}
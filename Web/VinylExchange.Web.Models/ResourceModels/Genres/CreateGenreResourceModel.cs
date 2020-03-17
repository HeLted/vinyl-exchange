namespace VinylExchange.Web.Models.ResourceModels.Genres
{
    #region

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class CreateGenreResourceModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
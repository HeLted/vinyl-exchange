namespace VinylExchange.Web.Models.InputModels.Styles
{
    #region

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class CreateStyleInputModel : IMapTo<Style>
    {
        public int GenreId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
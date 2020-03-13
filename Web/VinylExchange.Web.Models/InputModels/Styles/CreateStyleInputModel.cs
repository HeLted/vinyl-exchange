using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Web.Models.InputModels.Styles
{
    public class CreateStyleInputModel:IMapTo<Style>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int GenreId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ResourceModels.Genres
{
    public class GetAllGenresResourceModel : IMapFrom<Genre>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

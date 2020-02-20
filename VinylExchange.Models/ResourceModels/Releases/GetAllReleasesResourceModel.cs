using System;
using VinylExchange.Services.Mapping;
using VinylExchange.Models;
using VinylExchange.Data.Models;
using System.Collections.Generic;
using VinylExchange.Models.ResourceModels.ReleaseFiles;

namespace VinylExchange.Models.ResourceModels.Releases
{
    public class GetAllReleasesResourceModel : IMapFrom<Release>
    {
        public Guid Id { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Format { get; set; }
        public string Year { get; set; }
        public string Label { get; set; }    
        public ReleaseFileResourceModel CoverArt { get; set; }
       
    }
}

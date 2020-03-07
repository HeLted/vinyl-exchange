namespace VinylExchange.Web.Models.InputModels.Releases
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    public class CreateReleaseInputModel : IMapTo<Release>
    {
        [Required]
        public string Artist { get; set; }

        [Required]
        public string Format { get; set; }

        [Required]
        public string Label { get; set; }

        public ICollection<int> StyleIds { get; set; } = new HashSet<int>();

        [Required]
        public string Title { get; set; }

        [Required]
        public string Year { get; set; }
    }
}
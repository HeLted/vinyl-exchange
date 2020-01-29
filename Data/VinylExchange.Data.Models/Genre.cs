using System;
using System.Collections.Generic;
using System.Text;

namespace VinylExchange.Data.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Style> Styles { get; set; } = new HashSet<Style>();

    }
}

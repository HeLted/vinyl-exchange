using System;
using System.Collections.Generic;
using System.Text;

namespace VinylExchange.Data.Models
{
    public class StyleRelease
    {
        public int StyleId { get; set; }
        public Style Style { get; set; }
        public Guid ReleaseId { get; set; }
        public Release Release { get; set; }
    }
}

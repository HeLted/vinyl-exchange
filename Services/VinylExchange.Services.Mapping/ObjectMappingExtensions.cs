using System;
using System.Collections.Generic;
using System.Text;

namespace VinylExchange.Services.Mapping
{
    public static class ObjectMappingExtensions
    {
        public static TDestination To<TDestination>(this object source )
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return AutoMapperConfig.MapperInstance.Map<TDestination>(source);
        }
    }
}

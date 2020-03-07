using AutoMapper;

namespace VinylExchange.Services.Mapping
{   
    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}

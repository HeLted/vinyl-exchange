namespace VinylExchange.Services.Mapping
{
    #region

    using AutoMapper;

    #endregion

    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}
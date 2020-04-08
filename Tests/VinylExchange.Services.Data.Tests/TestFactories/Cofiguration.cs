#region

using Xunit;

#endregion

[assembly:
    TestFramework("VinylExchange.Services.Data.Tests.TestFactories.Configuration", "VinylExchange.Services.Data.Tests")]

namespace VinylExchange.Services.Data.Tests.TestFactories
{
    #region

    using System.Reflection;

    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models;

    using Xunit.Abstractions;
    using Xunit.DependencyInjection;

    #endregion

    internal class Configuration : DependencyInjectionTestFramework
    {
        public Configuration(IMessageSink messageSink)
            : base(messageSink)
        {
            AutoMapperConfig.RegisterMappings(typeof(ModelGetAssemblyClass).GetTypeInfo().Assembly);
        }
    }
}
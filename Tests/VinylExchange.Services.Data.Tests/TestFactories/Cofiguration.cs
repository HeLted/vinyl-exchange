using Xunit;
[assembly:
    TestFramework("VinylExchange.Services.Data.Tests.TestFactories.Configuration", "VinylExchange.Services.Data.Tests")]

namespace VinylExchange.Services.Data.Tests.TestFactories
{
    using System.Reflection;
    using Mapping;
    using Web.Models;
    using Xunit.Abstractions;
    using Xunit.DependencyInjection;

    internal class Configuration : DependencyInjectionTestFramework
    {
        public Configuration(IMessageSink messageSink)
            : base(messageSink)
        {
            AutoMapperConfig.RegisterMappings(typeof(ModelGetAssemblyClass).GetTypeInfo().Assembly);
        }
    }
}
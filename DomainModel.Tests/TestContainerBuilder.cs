using LabCommsModel.Design1;
using Envivo.Fresnel.Bootstrap;
using Envivo.Fresnel.Core;
using Microsoft.Extensions.DependencyInjection;

namespace LabCommsModel.Tests
{
    public static class TestContainerBuilder
    {
        public static IServiceProvider Build()
        {
            var additionalServices = new ServiceCollection();
            // Add your custom services as necessary

            var containerBuilder =
                new FresnelContainerBuilder()
                .WithModelAssemblyFrom<TestRequest>()
                .WithServices(additionalServices)
                .AbsorbServiceCollection()
                .InitialiseServiceCollection();

            var result = containerBuilder.BuildServiceProvider();

            var engine = result.GetService<Engine>();
            if (engine != null)
            {
                engine.InitialiseAssemblyReaders();
            }

            return result;
        }
    }
}

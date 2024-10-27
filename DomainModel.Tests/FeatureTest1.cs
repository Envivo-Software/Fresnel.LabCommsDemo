using LabCommsModel.Design1.Dependencies;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using Microsoft.Extensions.DependencyInjection;

namespace LabCommsModel.Tests
{
    [Label("FEAT-1")]
    [FeatureDescription(
@"In order to ____
As a ____
I want to ____")]
    public partial class FeatureTest1 
    {
        [Scenario]
        public void Scenario_1()
        {
            var container = TestContainerBuilder.Build();
            var repo = container.GetService<SampleRepository>();

            Runner.RunScenario(
                Given_some_condition,
                When_an_action_takes_place,
                Then_there_should_be_an_expected_outcome);
        }
    }
}
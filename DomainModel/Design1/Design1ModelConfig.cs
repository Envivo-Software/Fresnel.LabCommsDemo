using System.ComponentModel.DataAnnotations;
using Design1.Dependencies;
using Envivo.Fresnel.ModelAttributes;
using Envivo.Fresnel.ModelAttributes.Config;
using LabCommsModel.Design1.Dependencies;

namespace LabCommsModel.Design1
{
    public class Design1ModelConfig : ModelConfigBase
    {
        /// <inheritdoc/>
        public override void Configure()
        {
            ConfigureSample();
            ConfigureTestRequest();
            ConfigureTestResult();
            ConfigureAddTestRequestCommand();
        }

        private void ConfigureSample()
        {
            ConfigureClass<Sample>()
                .WithAttributes(new UiExplorerAttribute(UiExplorerSize.Maximised))

                .Property(o => o.TestRequests,
                    new RelationshipAttribute(RelationshipType.Owns),
                    new UIAttribute(UiRenderOption.InlineExpanded),
                    new AllowedOperationsAttribute(canCreate: false, canAdd: false),
                    new CollectionAttribute(
                        removeMethodName: nameof(Sample.RemoveTestRequest),
                        visibleColumnNames: [
                            nameof(TestRequest.RequestId),
                            nameof(TestRequest.TargetLab),
                            nameof(TestRequest.SentToLabAt),
                            nameof(TestRequest.Is_B_Sample),
                            nameof(TestRequest.Comments),
                        ])
                    )

                .Property(o => o.TestResults,
                    new RelationshipAttribute(RelationshipType.Owns),
                    new UIAttribute(UiRenderOption.InlineExpanded),
                    new AllowedOperationsAttribute(canCreate: false, canAdd: false),
                    new CollectionAttribute(
                        removeMethodName: nameof(Sample.RemoveTestRequest),
                        visibleColumnNames: [
                            nameof(TestResult.RequestId),
                            nameof(TestResult.TestRequest),
                            nameof(TestResult.ReceivedFromLabAt),
                            nameof(TestResult.Result),
                            nameof(TestResult.Comments),
                        ])
                    )

                .Method(o => o.AddTestRequest,
                    new MethodAttribute(relatedPropertyName: nameof(Sample.TestRequests))
                    )
                .Method(o => o.RemoveTestRequest,
                    new VisibleAttribute(false)
                    )
                .Method(o => o.ReceiveTestResult,
                    new MethodAttribute(relatedPropertyName: nameof(Sample.TestResults))
                    )
                    .MethodParameter(o => o.ReceiveTestResult,
                        "originalRequest",
                        new FilterQuerySpecificationAttribute(typeof(SampleTestResultOriginalRequestQuerySpecification)),
                        new UIAttribute(preferredControl: UiControlType.Select),
                        new RequiredAttribute()
                        )
                    .MethodParameter(o => o.ReceiveTestResult,
                        "resultType",
                        new RequiredAttribute()
                        )
                    .MethodParameter(o => o.ReceiveTestResult,
                        "optionalComment",
                        new UIAttribute(preferredControl: UiControlType.TextArea)
                )
                ;
        }

        private void ConfigureTestRequest()
        {
            ConfigureClass<TestRequest>()

                .Property(o => o.TargetLab,
                    new RelationshipAttribute(RelationshipType.Has),
                    new FilterQuerySpecificationAttribute(typeof(TargetLabQuerySpecification)),
                    new UIAttribute(UiRenderOption.InlineSimple, preferredControl: UiControlType.Select)
                    )

                .Property(o => o.Comments,
                    new UIAttribute(preferredControl: UiControlType.TextArea)
                    )

                .Property(o => o.Sample,
                    new RelationshipAttribute(RelationshipType.OwnedBy)
                    )
                ;
        }

        private void ConfigureTestResult()
        {
            ConfigureClass<TestResult>()

                .Property(o => o.TestRequest,
                    new RelationshipAttribute(RelationshipType.OwnedBy)
                    )
                ;
        }

        private void ConfigureAddTestRequestCommand()
        {
            ConfigureClass<AddTestRequestCommand>()

                .Property(o => o.Laboratory,
                    new FilterQuerySpecificationAttribute(typeof(TargetLabQuerySpecification)),
                    new UIAttribute(UiRenderOption.InlineSimple, preferredControl: UiControlType.Select)
                    )

                .Property(o => o.CommentsToLab,
                    new UIAttribute(preferredControl: UiControlType.TextArea)
                    )

                .Property(o => o.IsReadyToExecute,
                    new VisibleAttribute(false)
                    )
                ;
        }
    }
}

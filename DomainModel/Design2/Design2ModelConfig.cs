using Envivo.Fresnel.ModelAttributes;
using Envivo.Fresnel.ModelAttributes.Config;
using LabCommsModel.Design2.Messages;
using LabCommsModel.Design2.Messages.IncomingMessages;
using LabCommsModel.Design2.Messages.OutgoingMessages;

namespace LabCommsModel.Design2
{
    public class Design2ModelConfig : ModelConfigBase
    {
        /// <inheritdoc/>
        public override void Configure()
        {
            ConfigureSample();
            ConfigureTestingProcess();
            ConfigureICommsMessage();
            ConfigureIOutgoingMessage();
            ConfigureIIncomingMessage();
            ConfigureB_Sample_TransferRequest();
            ConfigureB_Sample_AdvanceNotification();

            ConfigureOverallResult();
        }

        private void ConfigureSample()
        {
            ConfigureClass<Sample>()

                .Property(o => o.TestingProcess,
                    new RelationshipAttribute(RelationshipType.Owns)
                    )
                ;
        }

        private void ConfigureTestingProcess()
        {
            ConfigureClass<TestingProcess>()
                .WithAttributes(new UiExplorerAttribute(UiExplorerSize.Maximised))

                .Property(o => o.Sample,
                    new RelationshipAttribute(RelationshipType.OwnedBy),
                    new UIAttribute(UiRenderOption.InlineSimple)
                    )

                .Property(o => o.OverallResult,
                    new UIAttribute(UiRenderOption.InlineSimple)
                    )

                .Property(o => o.Messages,
                    new RelationshipAttribute(RelationshipType.Owns),
                    new UIAttribute(UiRenderOption.InlineExpanded),
                    new CollectionAttribute(
                        canExpandRows: true,
                        addMethodName: nameof(TestingProcess.AddNewMessage),
                        visibleColumnNames: [
                            nameof(ICommsMessage.ExternalId),
                            nameof(ICommsMessage.Laboratory),
                            nameof(B_Sample_TransferRequest.TargetLaboratory),
                            nameof(B_Sample_AdvanceNotification.SourceLaboratory),
                            nameof(IOutgoingMessage.SentAt),
                            nameof(IIncomingMessage.ReceivedAt),
                            nameof(TestResult.Result),
                        ])
                    )

                .Method(o => o.TransferBSampleToAnotherLab,
                    new MethodAttribute(relatedPropertyName: nameof(TestingProcess.Messages))
                    )

                ;
        }

        private void ConfigureICommsMessage()
        {
            ConfigureClass<ICommsMessage>()
                .WithAttributes(new VisibleAttribute(isVisibleInLibrary: false))

                .Property(o => o.ExternalId,
                    new AllowedOperationsAttribute(canModify: false)
                    )

                .Property(o => o.Laboratory,
                    new UIAttribute(renderOption: UiRenderOption.InlineSimple, preferredControl: UiControlType.Select)
                    )

                .Property(o => o.Comments,
                    new UIAttribute(preferredControl: UiControlType.TextArea)
                    )
                ;
        }

        private void ConfigureIIncomingMessage()
        {
            ConfigureClass<IIncomingMessage>()
                .WithAttributes(new VisibleAttribute(isVisibleInLibrary: false))

                .Property(o => o.ReceivedAt,
                    new AllowedOperationsAttribute(canModify: false)
                    )
                ;
        }
        private void ConfigureIOutgoingMessage()
        {
            ConfigureClass<IOutgoingMessage>()
                .WithAttributes(new VisibleAttribute(isVisibleInLibrary: false))
                ;
        }

        private void ConfigureB_Sample_TransferRequest()
        {
            ConfigureClass<B_Sample_TransferRequest>()

                .Property(o => o.Laboratory,
                    new UIAttribute(renderOption: UiRenderOption.InlineSimple, preferredControl: UiControlType.Select)
                    )

                .Property(o => o.TargetLaboratory,
                    new UIAttribute(renderOption: UiRenderOption.InlineSimple, preferredControl: UiControlType.Select)
                    )
                ;
        }

        private void ConfigureB_Sample_AdvanceNotification()
        {
            ConfigureClass<B_Sample_AdvanceNotification>()

                .Property(o => o.Laboratory,
                    new UIAttribute(renderOption: UiRenderOption.InlineSimple, preferredControl: UiControlType.Select)
                    )

                .Property(o => o.SourceLaboratory,
                    new UIAttribute(renderOption: UiRenderOption.InlineSimple, preferredControl: UiControlType.Select)
                    )
                ;
        }

        private void ConfigureOverallResult()
        {
            ConfigureClass<OverallResult>()

                .Property(o => o.TestingProcess,
                    new RelationshipAttribute(RelationshipType.OwnedBy),
                    new UIAttribute(renderOption: UiRenderOption.SeparateTabExpanded)
                    )
                .Property(o => o.Reason,
                    new UIAttribute(preferredControl: UiControlType.TextArea)
                    )
                ;
        }

    }
}

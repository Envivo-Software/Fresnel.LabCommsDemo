using LabCommsModel.Design2.Messages.OutgoingMessages;
using Envivo.Fresnel.ModelAttributes;
using Envivo.Fresnel.ModelTypes.Interfaces;

namespace LabCommsModel.Design2
{
    /// <summary>
    /// Executes the process of transferring a B-Sample from one Lab to another
    /// </summary>
    public class BSampleTransferProcess :
        ICommandObject<TestingProcess>,
        IValueObject
    {
        public Guid Id { get; set; }

        /// <summary>
        /// The Laboratory that currently possesses the B-Sample
        /// </summary>
        [UI(UiRenderOption.InlineSimple, preferredControl: UiControlType.Select)]
        public Laboratory Laboratory { get; set; }

        /// <summary>
        /// Optional: Comments for the source Lab
        /// </summary>
        [UI(preferredControl: UiControlType.TextArea)]
        public string? CommentsToOriginatingLab { get; set; }

        /// <summary>
        /// The Laboratory that will take over the testing
        /// </summary>
        [UI(UiRenderOption.InlineSimple, preferredControl: UiControlType.Select)]
        public Laboratory TargetLaboratory { get; set; }

        /// <summary>
        /// Optional: Comments for the target Lab
        /// </summary>
        [UI(preferredControl: UiControlType.TextArea)]
        public string? CommentsToTargetLab { get; set; }

        /// <summary>
        /// Should the Sample be tested immediately?
        /// </summary>
        public bool ShouldTestOnArrival { get; set; }

        public bool IsReadyToExecute =>
            Laboratory != null &&
            TargetLaboratory != null;

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns></returns>
        public void Execute(TestingProcess testingProcess)
        {
            var externalId = testingProcess.Sample?.ExternalId;

            var transferRequest = CreateTransferRequest(externalId, Laboratory);
            testingProcess.Messages.Add(transferRequest);

            var advanceNotification = CreateIncomingSampleNotification(externalId, TargetLaboratory);
            testingProcess.Messages.Add(advanceNotification);

            if (ShouldTestOnArrival)
            {
                var testRequest = CreateBSampleTestRequest(externalId, TargetLaboratory);
                testingProcess.Messages.Add(testRequest);
            }
        }

        private B_Sample_TransferRequest CreateTransferRequest(string externalId, Laboratory laboratory)
        {
            var msg = new B_Sample_TransferRequest
            {
                Id = Guid.NewGuid(),
                ExternalId = externalId,
                Laboratory = laboratory,
                TargetLaboratory = TargetLaboratory,
                Comments = CommentsToOriginatingLab,
                SentAt = DateTime.Now,
            };

            return msg;
        }

        private B_Sample_AdvanceNotification CreateIncomingSampleNotification(string externalId, Laboratory targetLaboratory)
        {
            var msg = new B_Sample_AdvanceNotification
            {
                Id = Guid.NewGuid(),
                ExternalId = externalId,
                Laboratory = targetLaboratory,
                Comments = CommentsToTargetLab,
                SentAt = DateTime.Now,
            };

            return msg;
        }

        private B_Sample_TestRequest CreateBSampleTestRequest(string externalId, Laboratory targetLaboratory)
        {
            var msg = new B_Sample_TestRequest
            {
                Id = Guid.NewGuid(),
                ExternalId = externalId,
                Laboratory = targetLaboratory,
                Comments = CommentsToTargetLab,
                SentAt = DateTime.Now,
            };

            return msg;
        }
    }
}

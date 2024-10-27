using LabCommsModel.Design2.Messages;
using LabCommsModel.Design2.Messages.IncomingMessages;
using LabCommsModel.Design2.Messages.OutgoingMessages;
using Envivo.Fresnel.ModelAttributes;
using Envivo.Fresnel.ModelTypes.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabCommsModel.Design2
{
    /// <summary>
    /// Encapsulates the testing process, from start to finish
    /// </summary>
    public class TestingProcess : IAggregateRoot, IPersistable
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [ConcurrencyCheck]
        public long Version { get; set; }

        /// <summary>
        /// The Sample this conversation is associated with
        /// </summary>
        [Relationship(RelationshipType.OwnedBy)]
        [UI(UiRenderOption.InlineSimple)]
        [JsonInclude]
        public Sample? Sample { get; internal set; }

        /// <summary>
        /// The date/time when the Testing Process was initiated
        /// </summary>
        public DateTime InitiatedAt { get; internal set; }

        /// <summary>
        /// The final Test result 
        /// </summary>
        [JsonInclude]
        public TestResultType? Result { get; internal set; }

        ///// <summary>
        ///// The overall Test result 
        ///// </summary>
        //[UI(UiRenderOption.InlineSimple)]
        //[JsonInclude]
        //public OverallResult OverallResult { get; internal set; } = new OverallResult();

        ///// <summary>
        ///// The current state of progress
        ///// </summary>
        //public TestingProgressStatusType Status { get; set; }

        /// <summary>
        /// All messages to and from the Lab/s
        /// </summary>
        [Relationship(RelationshipType.Owns)]
        [UI(UiRenderOption.InlineExpanded)]
        [Collection(canExpandRows: true, addMethodName: nameof(AddNewMessage),
            VisibleColumnNames = [
                nameof(ICommsMessage.ExternalId),
                nameof(ICommsMessage.Laboratory),
                nameof(B_Sample_TransferRequest.TargetLaboratory),
                nameof(IOutgoingMessage.SentAt),
                nameof(IIncomingMessage.ReceivedAt),
                nameof(TestResult.Result),
        ])]
        [JsonInclude]
        public ICollection<ICommsMessage> Messages { get; internal set; } = [];

        public ICommsMessage AddNewMessage(ICommsMessage message)
        {
            message.ExternalId = Sample?.ExternalId;
            Messages.Add(message);

            // HACK (sorry!)
            if (message is IIncomingMessage incomingMessage)
            {
                incomingMessage.ReceivedAt = DateTime.Now;
            }

            return message;
        }

        /// <summary>
        /// Simulate a result being received from the Lab
        /// </summary>
        [Method(relatedPropertyName: nameof(Messages))]
        public void ReceiveTestResult(
            [Required]
            [UI(preferredControl:UiControlType.Select)]
            Laboratory laboratory,

            [Required]
            TestResultType resultType,

            [UI(preferredControl: UiControlType.TextArea)]
            string optionalComment
        )
        {
            var testResult = new TestResult
            {
                Id = Guid.NewGuid(),
                ExternalId = Sample?.ExternalId,
                ReceivedAt = DateTime.Now,
                Laboratory = laboratory,
                Result = resultType,
                Comments = optionalComment
            };

            Messages.Add(testResult);
        }

        /// <summary>
        /// Starts the process for transferring a B-Sample between Labs
        /// </summary>
        /// <returns></returns>
        [Method(relatedPropertyName: nameof(Messages))]
        public BSampleTransferProcess TransferBSampleToAnotherLab()
        {
            return new BSampleTransferProcess();
        }

        public override string ToString()
        {
            return $"{Sample} ({Messages?.Count()} messages)";
        }

    }
}
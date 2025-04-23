using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Envivo.Fresnel.ModelTypes.Interfaces;
using LabCommsModel.Design2.Messages;
using LabCommsModel.Design2.Messages.IncomingMessages;

namespace LabCommsModel.Design2
{
    /// <summary>
    /// Encapsulates the testing process, from start to finish
    /// </summary>
    public class TestingProcess : IAggregateRoot, IPersistable
    {
        public TestingProcess()
        {
            OverallResult = new OverallResult(this);
        }

        #region Fresnel attributes
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

        #endregion

        /// <summary>
        /// The Sample this conversation is associated with
        /// </summary>
        [JsonInclude]
        public Sample? Sample { get; internal set; }

        /// <summary>
        /// The date/time when the Testing Process was initiated
        /// </summary>
        public DateTime InitiatedAt { get; internal set; }

        /// <summary>
        /// The current state of progress
        /// </summary>
        internal TestingProgressStatusType Status { get; set; }

        /// <summary>
        /// All messages to and from the Lab/s
        /// </summary>
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
        /// The latest Test result 
        /// </summary>
        public TestResultType? Result =>
            this.Messages?
            .OfType<TestResult>()
            .LastOrDefault()?
            .Result;

        /// <summary>
        /// The overall Test result 
        /// </summary>
        [JsonInclude]
        public OverallResult OverallResult { get; set; }

        /// <summary>
        /// Starts the process for transferring a B-Sample between Labs
        /// </summary>
        /// <returns></returns>
        internal BSampleTransferProcess TransferBSampleToAnotherLab()
        {
            return new BSampleTransferProcess();
        }

        public override string ToString()
        {
            return $"{Sample} ({Messages?.Count()} messages)";
        }

    }
}

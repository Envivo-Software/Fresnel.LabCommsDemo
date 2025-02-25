using Envivo.Fresnel.ModelAttributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabCommsModel.Design2.Messages.IncomingMessages
{
    /// <summary>
    /// The results of a Test conducted by the Laboratory
    /// </summary>
    public class TestResult : IIncomingMessage
    {
        #region Fresnel attributes
        /// <inheritdoc/>
        [Key]
        public Guid Id { get; set; }

        /// <inheritdoc/>
        [ConcurrencyCheck]
        public long Version { get; set; }

        #endregion

        /// <summary>
        /// The public facing ID for the associated Sample
        /// </summary>
        public string? ExternalId { get; set; }

        /// <summary>
        /// The time when the message was delivered
        /// </summary>
        public DateTime ReceivedAt { get; set; }

        /// <inheritdoc>
        public DateTime? MessageDate => ReceivedAt;

        /// <summary>
        /// The Laboratory associated with this Message
        /// </summary>
        public Laboratory Laboratory { get; set; }

        /// <inheritdoc/>
        [UI(preferredControl: UiControlType.TextArea)]
        public string? Comments { get; set; }

        /// <summary>
        /// The findings reported by the lab
        /// </summary>
        [JsonInclude]
        public TestResultType Result { get; set; }
    }
}

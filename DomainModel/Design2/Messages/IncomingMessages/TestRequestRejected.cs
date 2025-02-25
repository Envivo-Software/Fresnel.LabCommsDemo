using Envivo.Fresnel.ModelAttributes;
using System.ComponentModel.DataAnnotations;

namespace LabCommsModel.Design2.Messages.IncomingMessages
{
    /// <summary>
    /// The Lab cannot (or will not) process the associated Test Request
    /// </summary>
    public class TestRequestRejected : IIncomingMessage
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

        /// <summary>
        /// The reason the Test Request was rejected
        /// </summary>
        [UI(preferredControl: UiControlType.TextArea)]
        public string? Comments { get; set; }
    }
}

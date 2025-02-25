using Envivo.Fresnel.ModelAttributes;
using System.ComponentModel.DataAnnotations;

namespace LabCommsModel.Design2.Messages.OutgoingMessages
{
    /// <summary>
    /// Request to run Tests against the A-Sample
    /// </summary>
    public class A_Sample_TestRequest : IOutgoingMessage
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
        /// The time when the message was sent
        /// </summary>
        public DateTime? SentAt { get; internal set; }

        /// <inheritdoc>
        public DateTime? MessageDate => SentAt;

        /// <summary>
        /// The Laboratory associated with this Message
        /// </summary>
        public Laboratory Laboratory { get;  set; }

        /// <inheritdoc/>
        [UI(preferredControl: UiControlType.TextArea)]
        public string? Comments { get; set; }

        /// <summary>
        /// Sends this message to the Lab
        /// </summary>
        public void SendToLab()
        {
            if (SentAt != null)
                return;

            SentAt = DateTime.Now;
        }
    }
}

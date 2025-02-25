using Envivo.Fresnel.ModelAttributes;
using System.ComponentModel.DataAnnotations;

namespace LabCommsModel.Design2.Messages.OutgoingMessages
{
    /// <summary>
    /// Request to Transfer the B-Sample to another Laboratory
    /// </summary>
    public class B_Sample_TransferRequest : IOutgoingMessage
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
        /// The Laboratory that currently possesses the B-Sample
        /// </summary>
        public Laboratory Laboratory { get; set; }

        /// <summary>
        /// The Laboratory that will take over the testing
        /// </summary>
        [UI(UiRenderOption.InlineSimple, preferredControl: UiControlType.Select)]
        public Laboratory TargetLaboratory { get; set; }

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

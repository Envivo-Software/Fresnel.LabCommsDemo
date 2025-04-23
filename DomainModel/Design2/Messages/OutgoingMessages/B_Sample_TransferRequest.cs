using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Envivo.Fresnel.ModelAttributes;

namespace LabCommsModel.Design2.Messages.OutgoingMessages
{
    /// <summary>
    /// Request to Transfer the B-Sample to another Laboratory
    /// </summary>
    [DisplayName("Out: B-Sample Transfer Request")]
    [Visible(isVisibleInLibrary: true)]
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
        public Laboratory TargetLaboratory { get; set; }

        /// <inheritdoc/>
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

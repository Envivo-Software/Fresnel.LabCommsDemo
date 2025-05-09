﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Envivo.Fresnel.ModelAttributes;

namespace LabCommsModel.Design2.Messages.OutgoingMessages
{
    /// <summary>
    /// Used to notify a Lab that a B-Sample will be arriving
    /// </summary>
    [DisplayName("Out: B-Sample Advance Notification")]
    [Visible(isVisibleInLibrary: true)]
    public class B_Sample_AdvanceNotification : IOutgoingMessage
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
        /// The Laboratory that will receive the B-Sample
        /// </summary>
        public Laboratory Laboratory { get; set; }

        /// <summary>
        /// The Laboratory that the sample will arrive from
        /// </summary>
        public Laboratory SourceLaboratory { get; set; }

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

using LabCommsModel.Design1.Dependencies;
using Envivo.Fresnel.ModelAttributes;
using Envivo.Fresnel.ModelTypes.Interfaces;
using System.Text.Json.Serialization;

namespace LabCommsModel.Design1
{
    /// <summary>
    /// A request sent to a Lab
    /// </summary>
    public record TestRequest : IEntity
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The public facing ID for this Test Request
        /// </summary>
        [JsonInclude]
        public long RequestId { get; internal set; } = Environment.TickCount;

        /// <summary>
        /// The Lab that should conduct this Test
        /// </summary>
        [Relationship(RelationshipType.Has)]
        [FilterQuerySpecification(typeof(TargetLabQuerySpecification))]
        [UI(UiRenderOption.InlineSimple, preferredControl: UiControlType.Select)]
        [JsonInclude]
        public Laboratory? TargetLab { get; set; }

        /// <summary>
        /// Is the request for the B Sample?
        /// </summary>
        public bool Is_B_Sample { get; set; }

        /// <summary>
        /// Optional: Any comments or instructions for the Lab 
        /// </summary>
        [UI(preferredControl: UiControlType.TextArea)]
        public string? Comments { get; set; }

        /// <summary>
        /// The Sample this Test Request is associated with
        /// </summary>
        [Relationship(RelationshipType.OwnedBy)]
        [JsonInclude]
        public Sample? Sample { get; internal set; }

        /// <summary>
        /// The date/time when this Request was sent to the Lab
        /// </summary>
        [JsonInclude]
        public DateTime? SentToLabAt { get; private set; }

        /// <summary>
        /// Simulate the request being sent to the Lab
        /// </summary>
        public void SendTestRequest()
        {
            if (SentToLabAt != null)
                throw new ApplicationException("The request was already sent to the Lab");

            SentToLabAt = DateTime.Now;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Sample?.ExternalId}/{RequestId}, {TargetLab}, Sent at {SentToLabAt?.ToString("s")}";
        }
    }
}
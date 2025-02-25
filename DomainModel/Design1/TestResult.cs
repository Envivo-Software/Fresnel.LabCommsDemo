using Envivo.Fresnel.ModelAttributes;
using Envivo.Fresnel.ModelTypes.Interfaces;
using System.Text.Json.Serialization;

namespace LabCommsModel.Design1
{
    /// <summary>
    /// The result of the Test returned from the Lab
    /// </summary>
    public class TestResult : IEntity, IValueObject
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The public facing ID for this Test Request
        /// </summary>
        [JsonInclude]
        public long RequestId { get; internal set; }

        /// <summary>
        /// The Test Request that this Result is associated with
        /// </summary>
        [Relationship(RelationshipType.OwnedBy)]
        [JsonInclude]
        public TestRequest TestRequest { get; internal set; }

        /// <summary>
        /// The date/time when this Result was received from the Lab
        /// </summary>
        [JsonInclude]
        public DateTime? ReceivedFromLabAt { get; internal set; }

        /// <summary>
        /// The findings reported by the lab
        /// </summary>
        [JsonInclude]
        public TestResultType Result { get; internal set; }

        /// <summary>
        /// Optional: Any comments or instructions from the Lab 
        /// </summary>
        [JsonInclude]
        public string? Comments { get; internal set; }

        public override string ToString()
        {
            return $"{Result}";
        }
    }
}

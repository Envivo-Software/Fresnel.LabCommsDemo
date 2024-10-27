using Envivo.Fresnel.ModelAttributes;
using Envivo.Fresnel.ModelTypes.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabCommsModel.Design2
{
    /// <summary>
    /// The overall result of the Testing Process for a Sample
    /// </summary>
    public class OverallResult : IEntity
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The Sample this conversation is associated with
        /// </summary>
        [Relationship(RelationshipType.OwnedBy)]
        [UI(UiRenderOption.SeparateTabExpanded)]
        [JsonInclude]
        public TestingProcess TestingProcess { get; internal set; }

        /// <summary>
        /// The final Test result 
        /// </summary>
        [JsonInclude]
        public TestResultType? Conclusion { get; set; }

        /// <summary>
        /// Optional: The reason for the conclusion
        /// </summary>
        [UI(preferredControl: UiControlType.TextArea)]
        public string Reason { get; set; }

        public string ApprovedBy { get; private set; }

        public DateTime ApprovalDate { get; private set; }

        public void Approve(string approverName)
        {
            ApprovedBy = approverName;
            ApprovalDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Conclusion} {Reason}";
        }
    }
}
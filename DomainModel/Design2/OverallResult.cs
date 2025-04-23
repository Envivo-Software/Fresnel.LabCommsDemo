using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Envivo.Fresnel.ModelTypes.Interfaces;

namespace LabCommsModel.Design2
{
    /// <summary>
    /// The overall result of the Testing Process for a Sample
    /// </summary>
    public class OverallResult : IEntity
    {
        public OverallResult() { }

        public OverallResult(TestingProcess testingProcess)
        {
            TestingProcess = testingProcess;
        }

        #region Fresnel attributes
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        #endregion

        /// <summary>
        /// The Sample this conversation is associated with
        /// </summary>
        [JsonInclude]
        public TestingProcess TestingProcess { get; internal set; }

        /// <summary>
        /// The latest Test result 
        /// </summary>
        public TestResultType? Result => TestingProcess.Result;

        /// <summary>
        /// The overall final Test result 
        /// </summary>
        [JsonInclude]
        public TestResultType? Conclusion { get; set; }

        /// <summary>
        /// The Testing Process was in-conclusive
        /// </summary>
        public bool? MarkedAsVoid { get; set; }

        /// <summary>
        /// Optional: The reason for the conclusion
        /// </summary>
        public string Reason { get; set; }

        public string ApprovedBy { get; private set; }

        public DateTime ApprovalDate { get; private set; }

        public void Approve(string approverName)
        {
            if (!string.IsNullOrEmpty(ApprovedBy))
            {
                throw new ApplicationException("This Result has already been approved");
            }

            ApprovedBy = approverName;
            ApprovalDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Conclusion} {Reason}";
        }
    }
}

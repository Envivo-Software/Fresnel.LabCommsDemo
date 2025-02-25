using LabCommsModel.Design1.Dependencies;
using Envivo.Fresnel.ModelAttributes;
using Envivo.Fresnel.ModelTypes.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabCommsModel.Design1
{
    /// <summary>
    /// A Sample taken from an Athlete
    /// </summary>
    public class Sample : IAggregateRoot, IPersistable
    {
        #region Fresnel attributes
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [ConcurrencyCheck]
        public long Version { get; set; }

        #endregion

        /// <summary>
        /// The public facing ID for this Sample. This is to anonymise the athlete.
        /// </summary>
        public string? ExternalId { get; set; }

        /// <summary>
        /// The medium for this Sample
        /// </summary>
        public SampleType SampleType { get; set; }

        /// <summary>
        /// The final Test result 
        /// </summary>
        [JsonInclude]
        public TestResultType? Result { get; internal set; }

        //#region Test Collections

        /// <summary>
        /// The Test Requests sent to the Labs
        /// </summary>
        [Relationship(RelationshipType.Owns)]
        [UI(UiRenderOption.InlineExpanded)]
        [AllowedOperations(canCreate: false, canAdd: false)]
        [Collection(removeMethodName: nameof(RemoveTestRequest))]
        [JsonInclude]
        public ICollection<TestRequest> TestRequests { get; internal set; } = [];

        /// <summary>
        /// The Test Results received from the Labs
        /// </summary>
        [Relationship(RelationshipType.Owns)]
        [UI(UiRenderOption.InlineExpanded)]
        [AllowedOperations(canCreate: false, canAdd: false)]
        [JsonInclude]
        public ICollection<TestResult> TestResults { get; internal set; } = [];

        //#endregion

        #region Test Collection Mutations

        /// <summary>
        /// Adds a new Test Request for this Sample
        /// </summary>
        /// <returns></returns>
        [Method(relatedPropertyName: nameof(TestRequests))]
        internal TestRequest AddTestRequest()
        {
            var newTestRequest = new TestRequest
            {
                Sample = this
            };
            TestRequests.Add(newTestRequest);
            return newTestRequest;
        }

        /// <summary>
        /// Removes the given Test Request from this Sample
        /// </summary>
        /// <param name="testRequest"></param>
        [Visible(false)]
        internal void RemoveTestRequest(TestRequest testRequest)
        {
            TestRequests.Remove(testRequest);
            testRequest.Sample = null;
            testRequest.TargetLab = null;
        }

        /// <summary>
        /// Simulate a result being received from the Lab
        /// </summary>
        [Method(relatedPropertyName: nameof(TestResults))]
        internal void ReceiveTestResult(
            [FilterQuerySpecification(typeof(SampleTestResultOriginalRequestQuerySpecification))]
            [UI(preferredControl: UiControlType.Select)]
            [Required]
            TestRequest originalRequest,

            [Required]
            TestResultType resultType,

            [UI(preferredControl: UiControlType.TextArea)]
            string optionalComment
        )
        {
            var latestResult = new TestResult
            {
                Id = Guid.NewGuid(),
                ReceivedFromLabAt = DateTime.Now,
                RequestId = originalRequest.RequestId,
                TestRequest = originalRequest,
                Result = resultType,
                Comments = optionalComment
            };

            TestResults.Add(latestResult);

            // Make it easy to identify the Test Result for this Sample:
            Result = resultType;
        }

        #endregion
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Envivo.Fresnel.ModelAttributes;
using Envivo.Fresnel.ModelTypes.Interfaces;

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
        /// The Test Requests sent to the Labs
        /// </summary>
        [JsonInclude]
        public ICollection<TestRequest> TestRequests { get; internal set; } = [];

        /// <summary>
        /// The Test Results received from the Labs
        /// </summary>
        [JsonInclude]
        public ICollection<TestResult> TestResults { get; internal set; } = [];

        /// <summary>
        /// The final Test result 
        /// </summary>
        public TestResultType? Result =>
            this.TestResults?
            .LastOrDefault()?
            .Result;

        /// <summary>
        /// Adds a new Test Request for this Sample
        /// </summary>
        /// <returns></returns>
        public AddTestRequestCommand AddTestRequest()
        {
            return new AddTestRequestCommand();
        }

        /// <summary>
        /// Removes the given Test Request from this Sample
        /// </summary>
        /// <param name="testRequest"></param>
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
        public void ReceiveTestResult(
            TestRequest originalRequest,
            TestResultType resultType,
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
        }
    }
}

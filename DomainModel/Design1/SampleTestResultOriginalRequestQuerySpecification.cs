using Envivo.Fresnel.ModelTypes.Interfaces;

namespace LabCommsModel.Design1.Dependencies
{

    /// <summary>
    /// Returns the list of TestRequests for a given Sample
    /// </summary>
    public class SampleTestResultOriginalRequestQuerySpecification : IQuerySpecification<TestRequest>
    {
        public Task<IEnumerable<TestRequest>> GetResultsAsync()
        {
            return Task.FromResult(new List<TestRequest>().AsEnumerable());
        }

        public Task<IEnumerable<TestRequest>> GetResultsAsync(Sample sample)
        {
            return Task.FromResult(sample.TestRequests.AsEnumerable());
        }
    }
}
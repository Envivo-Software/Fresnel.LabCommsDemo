using Envivo.Fresnel.ModelTypes.Interfaces;

namespace LabCommsModel.Design1.Dependencies
{

    /// <summary>
    /// Returns a list of Laboratories that Test Requests may be sent to
    /// </summary>
    /// <param name="labRepository"></param>
    public class TargetLabQuerySpecification(LabRepository labRepository) : IQuerySpecification<Laboratory>
    {
        /// <summary>
        /// Returns a list of Laboratories that Test Requests may be sent to
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Laboratory>> GetResultsAsync()
        {
            return Task.FromResult(labRepository.GetQuery().AsEnumerable());
        }
    }
}
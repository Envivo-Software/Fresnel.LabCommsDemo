using Envivo.Fresnel.ModelTypes.Interfaces;

namespace LabCommsModel.Design2.Dependencies
{
    /// <summary>
    /// A factory for <see cref="Sample"/>
    /// </summary>
    public class SampleFactory : IFactory<Sample>
    {
        private readonly string _ExternalId = "XRODQ";

        /// <summary>
        /// Returns an empty Sample
        /// </summary>
        /// <returns></returns>
        public Sample Create()
        {
            return new Sample();
        }

        /// <summary>
        /// Creates a pre-defined Sample to walk through scenarios
        /// </summary>
        /// <returns></returns>
        public Sample CreateForDemoWalkthrough()
        {
            return new Sample()
            {
                ExternalId = _ExternalId,
                SampleType = SampleType.Saliva,
            };
        }
    }
}
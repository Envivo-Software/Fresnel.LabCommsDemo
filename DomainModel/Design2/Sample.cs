using Envivo.Fresnel.ModelAttributes;
using Envivo.Fresnel.ModelTypes.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabCommsModel.Design2
{
    /// <summary>
    /// A Sample taken from an Athlete
    /// </summary>
    public class Sample : IAggregateRoot, IPersistable
    {
        /// <inheritdoc/>
        [Key]
        public Guid Id { get; set; }

        /// <inheritdoc/>
        [ConcurrencyCheck]
        public long Version { get; set; }

        /// <summary>
        /// The public facing ID for this Sample
        /// </summary>
        public string? ExternalId { get; set; }

        /// <summary>
        /// The medium for this Sample
        /// </summary>
        public SampleType SampleType { get; set; }

        /// <summary>
        /// Details of the Testing process
        /// </summary>
        [Relationship(RelationshipType.Owns)]
        [JsonInclude]
        public TestingProcess? TestingProcess { get; internal set; }

        /// <summary>
        /// Initiates the Testing Process
        /// </summary>
        /// <returns></returns>
        public TestingProcess StartTestingProcess()
        {
            var newTestingProcess = new TestingProcess
            {
                Id = Guid.NewGuid(),
                Sample = this,
                InitiatedAt = DateTime.Now,
            };

            TestingProcess = newTestingProcess;
            return newTestingProcess;
        }

        public override string ToString()
        {
            return $"{ExternalId}/{SampleType}";
        }
    }
}
using Envivo.Fresnel.ModelTypes.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LabCommsModel.Design2
{
    /// <summary>
    /// A Laboratory that will conduct tests on Samples
    /// </summary>
    public class Laboratory : IEntity, IPersistable
    {
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

        /// <summary>
        /// The name of this Laboratory
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The physical location of this Liab
        /// </summary>
        public string? Address { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
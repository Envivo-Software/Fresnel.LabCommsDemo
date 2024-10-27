using Envivo.Fresnel.ModelTypes;
using Envivo.Fresnel.ModelTypes.Interfaces;

namespace LabCommsModel.Design2.Dependencies
{
    /// <summary>
    /// A repository for <see cref="Laboratory"/>
    /// </summary>
    public class LabRepository : IRepository<Laboratory>
    {
        private InMemoryRepository<Laboratory> _InMemoryRepository = new(CreateDummyLaboratories());

        private static IEnumerable<Laboratory> CreateDummyLaboratories()
        {
            var results = new List<Laboratory> {
                new() {
                    Id = Guid.NewGuid(),
                    Name = "Laboratory 1",
                    Address = "123 Elm Street, Chicago"
                },

                new() {
                    Id = Guid.NewGuid(),
                    Name = "Laboratory 2",
                    Address = "234 Cheddar Grove, Florida"
                },

                new() {
                    Id = Guid.NewGuid(),
                    Name = "Laboratory 3",
                    Address = "345 Olive Close, Arizona"
                },
            };

            return results;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="aggregateRoot"></param>
        /// <returns></returns>
        public Task DeleteAsync(Laboratory aggregateRoot)
        {
            return _InMemoryRepository.DeleteAsync(aggregateRoot);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public IQueryable<Laboratory> GetQuery()
        {
            return _InMemoryRepository.GetQuery();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Laboratory?> LoadAsync(Guid id)
        {
            return await _InMemoryRepository.LoadAsync(id);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="aggregateRoot"></param>
        /// <param name="newObjects"></param>
        /// <param name="modifiedObjects"></param>
        /// <param name="deletedObjects"></param>
        /// <returns></returns>
        public Task<int> SaveAsync(Laboratory aggregateRoot, IEnumerable<object> newObjects, IEnumerable<object> modifiedObjects, IEnumerable<object> deletedObjects)
        {
            return _InMemoryRepository.SaveAsync(aggregateRoot, newObjects, modifiedObjects, deletedObjects);
        }
    }
}
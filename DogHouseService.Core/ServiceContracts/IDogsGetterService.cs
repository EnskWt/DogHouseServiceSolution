using DogHouseService.Core.DataTransferObjects.DogObjects;
using DogHouseService.Core.Enums;

namespace DogHouseService.Core.ServiceContracts
{
    /// <summary>
    /// Represents bussiness logic for retrieving dogs.
    /// </summary>
    public interface IDogsGetterService
    {
        /// <summary>
        /// Returns all dogs.
        /// </summary>
        /// <param name="sortAttribute">Attribute to sort by.</param>
        /// <param name="sortOrder">Order to sort by.</param>
        /// <param name="pageNumber">Page number of paginated list.</param>
        /// <param name="pageSize">Size of page for pagination.</param>
        /// <returns>Returns a list of dogs as a dog response.</returns>
        Task<List<DogResponse>> GetDogs(string? sortAttribute, SortOrderOptions? sortOrder, int pageNumber, int pageSize);

        /// <summary>
        /// Returns a dog by name.
        /// </summary>
        /// <param name="name">Name of dog</param>
        /// <returns>Returns matching dog as dog response.</returns>
        Task<DogResponse?> GetDogByName(string name);
    }
}

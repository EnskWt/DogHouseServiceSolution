using DogHouseService.Core.DataTransferObjects.DogObjects;
using DogHouseService.Core.Enums;

namespace DogHouseService.Core.ServiceContracts.DogsServicesContracts
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
        Task<List<DogResponse>> GetDogs(string sortAttribute, SortOrderOptions? sortOrder, int pageNumber, int pageSize);
    }
}

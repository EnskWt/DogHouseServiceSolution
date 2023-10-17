using DogHouseService.Core.Domain.Models;
using DogHouseService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogHouseService.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents a repository for dog entity.
    /// </summary>
    public interface IDogsRepository
    {
        /// <summary>
        /// Retrieves all dogs from the database as sorted and paginated list.
        /// </summary>
        /// <param name="sortAttribute">Attribute to sort by.</param>
        /// <param name="sortOrder">Order to sort by.</param>
        /// <param name="pageNumber">Page number of paginated list.</param>
        /// <param name="pageSize">Size of page for pagination.</param>
        /// <returns>Return a sorted and paginated list of Dog domain class.</returns>
        Task<List<Dog>> GetSortedAndPaginatedDogsAsync(string sortAttribute, SortOrderOptions? sortOrder, int pageNumber, int pageSize);

        /// <summary>
        /// Retrieves a dog from the database by name.
        /// </summary>
        /// <param name="name">Dog name.</param>
        /// <returns>Returns a dog from the database.</returns>
        Task<Dog?> GetDogByNameAsync(string name);

        /// <summary>
        /// Add a dog to the database.
        /// </summary>
        /// <param name="dog">Dog domain object that will be added to the database.</param>
        /// <returns>Return dog domain object.</returns>
        Task<Dog> AddDogAsync(Dog dog);
    }
}

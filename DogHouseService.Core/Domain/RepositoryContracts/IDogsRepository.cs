using DogHouseService.Core.Domain.Models;
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
        /// Retrieves a list of dogs from the database.
        /// </summary>
        /// <returns>Return all dogs from database.</returns>
        Task<List<Dog>> GetDogsAsync();

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

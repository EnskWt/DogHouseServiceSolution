using DogHouseService.Core.DataTransferObjects.DogObjects;
using DogHouseService.Core.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogHouseService.Core.ServiceContracts.DogsServicesContracts
{
    public interface IDogsFinderService
    {
        /// <summary>
        /// Returns a dog by name.
        /// </summary>
        /// <param name="name">Name of dog</param>
        /// <returns>Returns matching dog as dog response.</returns>
        Task<DogResponse?> FindDogByName(string name);

    }
}

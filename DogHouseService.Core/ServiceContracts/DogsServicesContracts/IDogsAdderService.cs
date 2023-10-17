using DogHouseService.Core.DataTransferObjects.DogObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogHouseService.Core.ServiceContracts.DogsServicesContracts
{
    /// <summary>
    /// Represents bussiness logic for adding dogs.
    /// </summary>
    public interface IDogsAdderService
    {
        /// <summary>
        /// Adds a new dog to list of dogs.
        /// </summary>
        /// <param name="dogAddRequest">Dog to add.</param>
        /// <returns>Returns a dog response as result, along with newly generated Id</returns>
        Task<DogResponse> AddDog(DogAddRequest? dogAddRequest);
    }
}

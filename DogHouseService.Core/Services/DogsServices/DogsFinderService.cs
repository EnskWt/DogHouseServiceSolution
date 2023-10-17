using DogHouseService.Core.DataTransferObjects.DogObjects;
using DogHouseService.Core.Domain.RepositoryContracts;
using DogHouseService.Core.ServiceContracts.DogsServicesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogHouseService.Core.Services.DogsServices
{
    public class DogsFinderService : IDogsFinderService
    {
        private readonly IDogsRepository _dogsRepository;

        public DogsFinderService(IDogsRepository dogsRepository)
        {
            _dogsRepository = dogsRepository;
        }

        public async Task<DogResponse?> FindDogByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Name can't be null.");
            }

            var dog = await _dogsRepository.GetDogByNameAsync(name);

            return dog?.ToDogResponse();
        }
    }
}

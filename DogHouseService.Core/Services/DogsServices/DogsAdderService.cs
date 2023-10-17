using DogHouseService.Core.DataTransferObjects.DogObjects;
using DogHouseService.Core.Domain.RepositoryContracts;
using DogHouseService.Core.ServiceContracts.DogsServicesContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogHouseService.Core.Services.DogsServices
{
    public class DogsAdderService : IDogsAdderService
    {
        private readonly IDogsRepository _dogsRepository;

        private readonly IDogsFinderService _dogsFinderService;

        public DogsAdderService(IDogsRepository dogsRepository, IDogsFinderService dogsFinderService)
        {
            _dogsRepository = dogsRepository;
            _dogsFinderService = dogsFinderService;
        }

        public async Task<DogResponse> AddDog(DogAddRequest? dogAddRequest)
        {
            if (dogAddRequest is null)
            {
                throw new ArgumentNullException("Dog add request can't be null.");
            }

            var isDogNameTaken = await _dogsFinderService.FindDogByName(dogAddRequest.Name) != null;
            if (isDogNameTaken)
            {
                throw new ArgumentException("Dog name is already taken.");
            }

            var addedDog = await _dogsRepository.AddDogAsync(dogAddRequest.ToDog());
            return addedDog.ToDogResponse();
        }
    }
}

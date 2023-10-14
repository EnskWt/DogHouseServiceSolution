using DogHouseService.Core.DataTransferObjects.DogObjects;
using DogHouseService.Core.Domain.RepositoryContracts;
using DogHouseService.Core.ServiceContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogHouseService.Core.Services
{
    public class DogsAdderService : IDogsAdderService
    {
        private readonly IDogsRepository _dogsRepository;

        private readonly IDogsGetterService _dogsGetterService;

        public DogsAdderService(IDogsRepository dogsRepository, IDogsGetterService dogsGetterService)
        {
            _dogsRepository = dogsRepository;
            _dogsGetterService = dogsGetterService;
        }

        public async Task<DogResponse> AddDog(DogAddRequest? dogAddRequest)
        {
            if (dogAddRequest is null)
            {
                throw new ArgumentNullException("Dog add request can't be null.");
            }
            
            var isDogNameTaken = await _dogsGetterService.GetDogByName(dogAddRequest.Name) != null;
            if (isDogNameTaken)
            {
                throw new ArgumentException("Dog name is already taken.");
            }

            var addedDog = await _dogsRepository.AddDogAsync(dogAddRequest.ToDog());
            return addedDog.ToDogResponse();
        }
    }
}

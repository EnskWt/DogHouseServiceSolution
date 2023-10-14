using DogHouseService.Core.DataTransferObjects.DogObjects;
using DogHouseService.Core.Domain.RepositoryContracts;
using DogHouseService.Core.Enums;
using DogHouseService.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using DogHouseService.Core.Domain.Models;

namespace DogHouseService.Core.Services
{
    public class DogsGetterService : IDogsGetterService
    {
        private readonly IDogsRepository _dogsRepository;

        public DogsGetterService(IDogsRepository dogsRepository)
        {
            _dogsRepository = dogsRepository;
        }

        public async Task<List<DogResponse>> GetDogs(string? sortAttribute, SortOrderOptions? sortOrder, int pageNumber, int pageSize)
        {
            if (string.IsNullOrEmpty(sortAttribute) || sortOrder == null )
            {
                throw new ArgumentNullException("Sort attribute and sort order can't be null.");
            }

            var dogs = await _dogsRepository.GetDogsAsync();

            dogs = dogs.AsQueryable().OrderBy($"{sortAttribute.Normalize()} {sortOrder}")
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return dogs.Select(dog => dog.ToDogResponse()).ToList();
        }

        public async Task<DogResponse?> GetDogByName(string name)
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

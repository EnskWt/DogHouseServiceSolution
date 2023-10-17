using DogHouseService.Core.DataTransferObjects.DogObjects;
using DogHouseService.Core.Domain.RepositoryContracts;
using DogHouseService.Core.Enums;
using DogHouseService.Core.ServiceContracts.DogsServicesContracts;
using System.Linq.Dynamic.Core;

namespace DogHouseService.Core.Services.DogsServices
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
            if (string.IsNullOrEmpty(sortAttribute) || sortOrder == null)
            {
                throw new ArgumentNullException("Sort attribute and sort order can't be null.");
            }

            if (pageNumber < 1 || pageSize < 1)
            {
                throw new ArgumentException("Page number and page size can't be less than 1.");
            }

            var dogs = await _dogsRepository.GetSortedAndPaginatedDogsAsync(sortAttribute, sortOrder, pageNumber, pageSize);

            return dogs.Select(dog => dog.ToDogResponse()).ToList();
        }
    }
}

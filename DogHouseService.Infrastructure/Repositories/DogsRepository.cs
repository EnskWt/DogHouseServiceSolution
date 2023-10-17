using DogHouseService.Core.Domain.Models;
using DogHouseService.Core.Domain.RepositoryContracts;
using DogHouseService.Core.Enums;
using DogHouseService.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace DogHouseService.Infrastructure.Repositories
{
    public class DogsRepository : IDogsRepository
    {
        private ApplicationDbContext _db;

        public DogsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Dog>> GetSortedAndPaginatedDogsAsync(string sortAttribute, SortOrderOptions? sortOrder, int pageNumber, int pageSize)
        {
            var dogs = await _db.Dogs.OrderBy($"{sortAttribute.Normalize()} {sortOrder}")
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return dogs;
        }

        public async Task<Dog?> GetDogByNameAsync(string name)
        {
            var dog = await _db.Dogs.FirstOrDefaultAsync(d => d.Name == name);
            return dog;
        }

        public async Task<Dog> AddDogAsync(Dog dog)
        {
            await _db.Dogs.AddAsync(dog);
            await _db.SaveChangesAsync();

            return dog;
        }
    }
}

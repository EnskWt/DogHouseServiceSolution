using AutoFixture;
using DogHouseService.Core.DataTransferObjects.DogObjects;
using DogHouseService.Core.Domain.Models;
using DogHouseService.Core.Domain.RepositoryContracts;
using DogHouseService.Core.Enums;
using DogHouseService.Core.ServiceContracts.DogsServicesContracts;
using DogHouseService.Core.Services;
using DogHouseService.Core.Services.DogsServices;
using FluentAssertions;
using Moq;

namespace DogHouseService.ServiceTests
{
    public class DogsGetterServiceTests
    {
        private readonly IDogsGetterService _dogsGetterService;

        private readonly Mock<IDogsRepository> _dogsRepositoryMock;
        private readonly IDogsRepository _dogsRepository;

        private readonly IFixture _fixture;

        public DogsGetterServiceTests()
        {
            _fixture = new Fixture();

            _dogsRepositoryMock = new Mock<IDogsRepository>();
            _dogsRepository = _dogsRepositoryMock.Object;

            _dogsGetterService = new DogsGetterService(_dogsRepository);
        }

        [Fact]
        public async Task GetDogs_SortAttributeNull_ShouldReturnArgumentNullException()
        {
            // Arrange
            string? sortAttribute = null;
            SortOrderOptions? sortOrder = SortOrderOptions.ASC;
            int pageNumber = 1;
            int pageSize = 10;

            // Act
            Func<Task> action = async () =>
            {
                await _dogsGetterService.GetDogs(sortAttribute, sortOrder, pageNumber, pageSize);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetDogs_SortOrderNull_ShouldReturnArgumentNullException()
        {
            // Arrange
            string? sortAttribute = "Name";
            SortOrderOptions? sortOrder = null;
            int pageNumber = 1;
            int pageSize = 10;

            // Act
            Func<Task> action = async () =>
            {
                await _dogsGetterService.GetDogs(sortAttribute, sortOrder, pageNumber, pageSize);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetDogs_AllArgumentValid_ShouldReturnDogResponseList()
        {
            // Arrange
            string? sortAttribute = "Name";
            SortOrderOptions? sortOrder = SortOrderOptions.ASC;
            int pageNumber = 1;
            int pageSize = 10;

            var dogs = _fixture.CreateMany<Dog>().ToList();
            _dogsRepositoryMock.Setup(x => x.GetSortedAndPaginatedDogsAsync(It.IsAny<string>(), It.IsAny<SortOrderOptions>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(dogs);           

            // Act
            var result = await _dogsGetterService.GetDogs(sortAttribute, sortOrder, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<List<DogResponse>>();
        }
    }
}

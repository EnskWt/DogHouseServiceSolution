using AutoFixture;
using DogHouseService.Core.DataTransferObjects.DogObjects;
using DogHouseService.Core.Domain.Models;
using DogHouseService.Core.Domain.RepositoryContracts;
using DogHouseService.Core.Enums;
using DogHouseService.Core.ServiceContracts;
using DogHouseService.Core.Services;
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

        #region GetDogs

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
            _dogsRepositoryMock.Setup(x => x.GetDogsAsync()).ReturnsAsync(dogs);

            // Act
            var result = await _dogsGetterService.GetDogs(sortAttribute, sortOrder, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<List<DogResponse>>();
        }

        [Fact]
        public async Task GetDogs_PaginationFullPage_ReturnsDogsFromPage()
        {
            // Arrange
            string? sortAttribute = "Name";
            SortOrderOptions? sortOrder = SortOrderOptions.ASC;
            int pageNumber = 2;
            int pageSize = 5;

            var dogs = _fixture.CreateMany<Dog>(20).OrderBy(d => d.Name).ToList();
            _dogsRepositoryMock.Setup(x => x.GetDogsAsync()).ReturnsAsync(dogs);

            // Act
            var result = await _dogsGetterService.GetDogs(sortAttribute, sortOrder, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<List<DogResponse>>();
            result.Count.Should().Be(pageSize);
            result.First().Name.Should().Be(dogs[pageSize * (pageNumber - 1)].Name);
        }

        [Fact]
        public async Task GetDogs_PaginationNotFullPage_ReturnsDogsFromPage()
        {
            // Arrange
            string? sortAttribute = "Name";
            SortOrderOptions? sortOrder = SortOrderOptions.ASC;
            int pageNumber = 4;
            int pageSize = 6;

            var dogs = _fixture.CreateMany<Dog>(20).OrderBy(d => d.Name).ToList();
            _dogsRepositoryMock.Setup(x => x.GetDogsAsync()).ReturnsAsync(dogs);

            // Act
            var result = await _dogsGetterService.GetDogs(sortAttribute, sortOrder, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<List<DogResponse>>();
            result.Count.Should().Be(20 - (pageNumber - 1) * pageSize);
            result.First().Name.Should().Be(dogs[pageSize * (pageNumber - 1)].Name);
        }

        [Fact]
        public async Task GetDogs_SortOrderTest_ReturnsCorrectlySortedDogsByDescending()
        {
            // Arrange
            string? sortAttribute = "Name";
            SortOrderOptions? sortOrder = SortOrderOptions.DESC;
            int pageNumber = 1;
            int pageSize = 10;

            var dogs = _fixture.CreateMany<Dog>(20).ToList();
            _dogsRepositoryMock.Setup(x => x.GetDogsAsync()).ReturnsAsync(dogs);

            // Act
            var result = await _dogsGetterService.GetDogs(sortAttribute, sortOrder, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<List<DogResponse>>();

            result.Should().BeEquivalentTo(dogs.OrderByDescending(d => d.Name).Take(pageSize), options => options.Excluding(dog => dog.Id));
        }

        [Fact]
        public async Task GetDogs_SortOrderTest_ReturnsCorrectlySortedDogsByAscending()
        {
            // Arrange
            string? sortAttribute = "Name";
            SortOrderOptions? sortOrder = SortOrderOptions.ASC;
            int pageNumber = 1;
            int pageSize = 10;

            var dogs = _fixture.CreateMany<Dog>(20).ToList();
            _dogsRepositoryMock.Setup(x => x.GetDogsAsync()).ReturnsAsync(dogs);

            // Act
            var result = await _dogsGetterService.GetDogs(sortAttribute, sortOrder, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<List<DogResponse>>();

            result.Should().BeEquivalentTo(dogs.OrderBy(d => d.Name).Take(pageSize), options => options.Excluding(dog => dog.Id));
        }

        #endregion

        #region GetDogByName

        [Fact]
        public async Task GetDogByName_NameNull_ShouldReturnArgumentNullException()
        {
            // Arrange
            string name = null!;

            // Act
            Func<Task> action = async () =>
            {
                await _dogsGetterService.GetDogByName(name);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetDogByName_NameEmpty_ShouldReturnArgumentNullException()
        {
            // Arrange
            string name = string.Empty;

            // Act
            Func<Task> action = async () =>
            {
                await _dogsGetterService.GetDogByName(name);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetDogByName_DogFromRepositoryNotNull_ShouldReturnDogResponse()
        {
            // Arrange
            string name = "TestName";

            var dog = _fixture.Create<Dog>();
            _dogsRepositoryMock.Setup(x => x.GetDogByNameAsync(name)).ReturnsAsync(dog);

            // Act
            var result = await _dogsGetterService.GetDogByName(name);

            // Assert
            result.Should().BeOfType<DogResponse>();
            result.Should().BeEquivalentTo(dog.ToDogResponse());
        }

        [Fact]
        public async Task GetDogByName_DogFromRepositoryNull_ShouldReturnNull()
        {
            // Arrange
            string name = "TestName";

            _dogsRepositoryMock.Setup(x => x.GetDogByNameAsync(name)).ReturnsAsync(null as Dog);

            // Act
            var result = await _dogsGetterService.GetDogByName(name);

            // Assert
            result.Should().BeNull();
        }

        #endregion
    }
}

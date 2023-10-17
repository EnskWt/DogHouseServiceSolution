using AutoFixture;
using DogHouseService.Core.DataTransferObjects.DogObjects;
using DogHouseService.Core.Domain.Models;
using DogHouseService.Core.Domain.RepositoryContracts;
using DogHouseService.Core.ServiceContracts.DogsServicesContracts;
using DogHouseService.Core.Services.DogsServices;
using FluentAssertions;
using Moq;

namespace DogHouseService.ServiceTests
{
    public class DogsFinderServiceTests
    {
        private readonly IDogsFinderService _dogsFinderService;

        private readonly Mock<IDogsRepository> _dogsRepositoryMock;
        private readonly IDogsRepository _dogsRepository;

        private readonly IFixture _fixture;

        public DogsFinderServiceTests()
        {
            _fixture = new Fixture();

            _dogsRepositoryMock = new Mock<IDogsRepository>();
            _dogsRepository = _dogsRepositoryMock.Object;

            _dogsFinderService = new DogsFinderService(_dogsRepository);
        }

        [Fact]
        public async Task FindDogByName_NameNull_ShouldReturnArgumentNullException()
        {
            // Arrange
            string name = null!;

            // Act
            Func<Task> action = async () =>
            {
                await _dogsFinderService.FindDogByName(name);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task FindDogByName_NameEmpty_ShouldReturnArgumentNullException()
        {
            // Arrange
            string name = string.Empty;

            // Act
            Func<Task> action = async () =>
            {
                await _dogsFinderService.FindDogByName(name);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task FindDogByName_DogFromRepositoryNotNull_ShouldReturnDogResponse()
        {
            // Arrange
            string name = "TestName";

            var dog = _fixture.Create<Dog>();
            _dogsRepositoryMock.Setup(x => x.GetDogByNameAsync(name)).ReturnsAsync(dog);

            // Act
            var result = await _dogsFinderService.FindDogByName(name);

            // Assert
            result.Should().BeOfType<DogResponse>();
            result.Should().BeEquivalentTo(dog.ToDogResponse());
        }

        [Fact]
        public async Task FindDogByName_DogFromRepositoryNull_ShouldReturnNull()
        {
            // Arrange
            string name = "TestName";

            _dogsRepositoryMock.Setup(x => x.GetDogByNameAsync(name)).ReturnsAsync(null as Dog);

            // Act
            var result = await _dogsFinderService.FindDogByName(name);

            // Assert
            result.Should().BeNull();
        }
    }
}

using AutoFixture;
using DogHouseService.Core.DataTransferObjects.DogObjects;
using DogHouseService.Core.Domain.Models;
using DogHouseService.Core.Domain.RepositoryContracts;
using DogHouseService.Core.ServiceContracts.DogsServicesContracts;
using DogHouseService.Core.Services;
using DogHouseService.Core.Services.DogsServices;
using FluentAssertions;
using Moq;

namespace DogHouseService.ServiceTests
{
    public class DogsAdderServiceTests
    {
        private readonly IDogsAdderService _dogsAdderService;

        private readonly Mock<IDogsFinderService> _dogsFinderServiceMock;
        private readonly IDogsFinderService _dogsFinderService;

        private readonly Mock<IDogsRepository> _dogsRepositoryMock;
        private readonly IDogsRepository _dogsRepository;

        private readonly IFixture _fixture;

        public DogsAdderServiceTests()
        {
            _fixture = new Fixture();

            _dogsRepositoryMock = new Mock<IDogsRepository>();
            _dogsRepository = _dogsRepositoryMock.Object;

            _dogsFinderServiceMock = new Mock<IDogsFinderService>();
            _dogsFinderService = _dogsFinderServiceMock.Object;

            _dogsAdderService = new DogsAdderService(_dogsRepository, _dogsFinderService);
        }

        [Fact]
        public async Task AddDog_DogAddRequestNull_ShouldReturnArgumentNullException()
        {
            // Arrange
            DogAddRequest? dogAddRequest = null;

            // Act
            Func<Task> action = async () =>
            {
                await _dogsAdderService.AddDog(dogAddRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();            
        }

        [Fact]
        public async Task AddDog_DogAddRequestHasDuplicateName_ShouldReturnArgumentException()
        {
            // Arrange
            var dogAddRequest = _fixture.Create<DogAddRequest>();
            var exceptionDog = _fixture.Create<Dog>();
            _dogsFinderServiceMock.Setup(x => x.FindDogByName(It.IsAny<string>())).ReturnsAsync(exceptionDog.ToDogResponse());

            // Act
            Func<Task> action = async () =>
            {
                await _dogsAdderService.AddDog(dogAddRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();          
        }

        [Fact]
        public async Task AddDog_DogAddRequestIsValid_ShouldReturnDogResponse()
        {
            // Arrange
            var dogAddRequest = _fixture.Create<DogAddRequest>();
            var dog = dogAddRequest.ToDog();
            var dogResponse = dog.ToDogResponse();

            _dogsFinderServiceMock.Setup(x => x.FindDogByName(It.IsAny<string>())).ReturnsAsync(null as DogResponse);
            _dogsRepositoryMock.Setup(x => x.AddDogAsync(It.IsAny<Dog>())).ReturnsAsync(dog);

            // Act
            var result = await _dogsAdderService.AddDog(dogAddRequest);

            // Assert
            result.Should().BeOfType<DogResponse>();
            result.Should().BeEquivalentTo(dogResponse);
        }
    }
}

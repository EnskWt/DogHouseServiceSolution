using AutoFixture;
using DogHouseService.Core.DataTransferObjects.DogObjects;
using DogHouseService.Core.Enums;
using DogHouseService.Core.ServiceContracts.DogsServicesContracts;
using DogHouseService.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DogHouseService.ControllerTests
{
    public class DogsControllerTests
    {
        private readonly IFixture _fixture;

        private readonly Mock<IDogsGetterService> _dogsGetterServiceMock;
        private readonly IDogsGetterService _dogsGetterService;

        private readonly Mock<IDogsAdderService> _dogsAdderServiceMock;
        private readonly IDogsAdderService _dogsAdderService;

        private readonly DogsController _dogsController;

        public DogsControllerTests()
        {
            _fixture = new Fixture();

            _dogsGetterServiceMock = new Mock<IDogsGetterService>();
            _dogsGetterService = _dogsGetterServiceMock.Object;

            _dogsAdderServiceMock = new Mock<IDogsAdderService>();
            _dogsAdderService = _dogsAdderServiceMock.Object;

            _dogsController = new DogsController(_dogsGetterService, _dogsAdderService);
        }

        #region Ping
        [Fact]
        public async Task Ping_ShouldReturnOk()
        {
            // Arrange

            // Act
            var result = await _dogsController.Ping();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
        #endregion

        #region GetDogs

        [Fact]
        public async Task GetDogs_CommonCall_ShouldReturnOk()
        {
            // Arrange
            var dogsResponses = _fixture.CreateMany<DogResponse>().ToList();

            string? sortAttribute = "Name";
            SortOrderOptions sortOrder = SortOrderOptions.ASC;
            int pageNumber = 1;
            int pageSize = 10;

            _dogsGetterServiceMock.Setup(x => x.GetDogs(sortAttribute, sortOrder, pageNumber, pageSize))
                .ReturnsAsync(dogsResponses);

            // Act
            var result = await _dogsController.GetDogs(sortAttribute, sortOrder, pageNumber, pageSize);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeOfType<List<DogResponse>>();
        }

        #endregion

        #region AddDog

        [Fact]
        public async Task AddDog_DogAddRequestValid_ShouldReturnBadRequest()
        {
            // Arrange
            DogAddRequest? dogAddRequest = _fixture.Create<DogAddRequest>();

            _dogsAdderServiceMock.Setup(x => x.AddDog(dogAddRequest))
                .ReturnsAsync(dogAddRequest.ToDog().ToDogResponse());

            // Act
            var result = await _dogsController.AddDog(dogAddRequest);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeOfType<DogResponse>();
        }

        #endregion
    }
}

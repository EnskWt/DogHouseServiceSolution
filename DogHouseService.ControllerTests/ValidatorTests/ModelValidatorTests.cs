using AutoFixture;
using DogHouseService.Core.DataTransferObjects.DogObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogHouseService.ControllerTests.ValidatorTests
{
    public class ModelValidatorTests
    {
        private readonly IFixture _fixture;
        public ModelValidatorTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void DogAddRequest_InvalidModel_ShouldHaveModelStateErrors()
        {
            // Arrange
            var dogAddRequest = _fixture.Build<DogAddRequest>()
                .With(x => x.Name, null as string)
                .Create();
            var validationContext = new ValidationContext(dogAddRequest);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dogAddRequest, validationContext, validationResults, true);

            // Assert
            isValid.Should().BeFalse();
            validationResults.Should().Contain(r => r.ErrorMessage == "Name can't be blank.");
        }
    }
}

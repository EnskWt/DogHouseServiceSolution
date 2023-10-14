using DogHouseService.Core.Helpers;
using DogHouseService.Web.Filters.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.AspNetCore.Routing;
using FluentAssertions;

namespace DogHouseService.ControllerTests.ActionFiltersTests
{
    public class SortOptionsActionFilterTests
    {
        private readonly SortOptionsActionFilter _filter;
        private readonly ActionExecutingContext _context;

        public SortOptionsActionFilterTests()
        {
            _filter = new SortOptionsActionFilter();
            _context = new ActionExecutingContext(
                new ActionContext
                {
                    HttpContext = new DefaultHttpContext(),
                    RouteData = new RouteData(),
                    ActionDescriptor = new ControllerActionDescriptor()
                },
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<Controller>().Object);
        }

        [Fact]
        public async Task OnActionExecutionAsync_SortAttributeNotInOptions_ShouldSetToName()
        {
            // Arrange
            _context.ActionArguments["sortAttribute"] = "InvalidAttribute";

            // Act
            await _filter.OnActionExecutionAsync(_context, () => Task.FromResult(new ActionExecutedContext(_context, new List<IFilterMetadata>(), _context.Controller)));

            // Assert
            _context.ActionArguments["sortAttribute"].Should().Be("Name");
        }

        [Fact]
        public async Task OnActionExecutionAsync_SortAttributeInOptions_ShouldNotChange()
        {
            // Arrange
            var validSortAttribute = SortOptionsHelper.GetSortAttributeOptions().First();
            _context.ActionArguments["sortAttribute"] = validSortAttribute;

            // Act
            await _filter.OnActionExecutionAsync(_context, () => Task.FromResult(new ActionExecutedContext(_context, new List<IFilterMetadata>(), _context.Controller)));

            // Assert
            _context.ActionArguments["sortAttribute"].Should().Be(validSortAttribute);
        }
    }
}

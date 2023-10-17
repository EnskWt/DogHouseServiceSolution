using DogHouseService.Core.DataTransferObjects.DogObjects;
using DogHouseService.Core.Enums;
using DogHouseService.Core.ServiceContracts.DogsServicesContracts;
using DogHouseService.Web.Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DogHouseService.Web.Controllers
{
    [ApiController]
    [EnableRateLimiting("10RequestsLimiter")]
    [Route("/")]
    public class DogsController : ControllerBase
    {
        private readonly IDogsGetterService _dogsGetterService;
        private readonly IDogsAdderService _dogsAdderService;

        public DogsController(IDogsGetterService dogsGetterService, IDogsAdderService dogsAdderService)
        {
            _dogsGetterService = dogsGetterService;
            _dogsAdderService = dogsAdderService;
        }


        [HttpGet("ping")]
        public async Task<IActionResult> Ping()
        {
            return Ok("Dogshouseservice.Version1.0.1");
        }

        [HttpGet("dogs")]
        [TypeFilter(typeof(SortOptionsActionFilter))]
        public async Task<IActionResult> GetDogs(string sortAttribute = nameof(DogResponse.Name), SortOrderOptions sortOrder = SortOrderOptions.ASC, int pageNumber = 1, int pageSize = 10)
        {
            var dogs = await _dogsGetterService.GetDogs(sortAttribute, sortOrder, pageNumber, pageSize);
            return Ok(dogs);
        }

        [HttpPost("dog")]
        public async Task<IActionResult> AddDog([FromBody] DogAddRequest dogAddRequest)
        {
            var addedDog = await _dogsAdderService.AddDog(dogAddRequest);
            return Ok(addedDog);
        }
    }
}

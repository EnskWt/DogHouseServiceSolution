using DogHouseService.Core.Domain.Models;

namespace DogHouseService.Core.DataTransferObjects.DogObjects
{
    /// <summary>
    /// Class represents a dog response to a dog object.
    /// </summary>
    public class DogResponse
    {
        public string Name { get; set; } = null!;
        public string Color { get; set; } = null!;
        public int TailLength { get; set; } = 0;
        public int Weight { get; set; } = 0;
    }

    public static class DogResponseExtensions
    {
        /// <summary>
        /// Converts a dog domain class to a dog response.
        /// </summary>
        /// <param name="dog">Parameter for extension method.</param>
        /// <returns>Returns a dog response object.</returns>
        public static DogResponse ToDogResponse(this Dog dog)
        {
            return new DogResponse
            {
                Name = dog.Name,
                Color = dog.Color,
                TailLength = dog.TailLength,
                Weight = dog.Weight
            };
        }
    }

}

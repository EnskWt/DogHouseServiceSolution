using DogHouseService.Core.Domain.Models;
using System.ComponentModel.DataAnnotations;


namespace DogHouseService.Core.DataTransferObjects.DogObjects
{
    /// <summary>
    /// Represents a request to add a new dog. 
    /// </summary>
    public class DogAddRequest
    {
        [Required(ErrorMessage = "Name can't be blank.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Color can't be blank.")]
        public string Color { get; set; } = null!;

        [Range(0, int.MaxValue, ErrorMessage = "Tail length can't be a negative number.")]
        public int TailLength { get; set; } = 0;

        [Range(0, int.MaxValue, ErrorMessage = "Weight can't be a negative number.")]
        public int Weight { get; set; } = 0;


        /// <summary>
        /// Converts the dog add request class to a domain dog class.
        /// </summary>
        /// <returns>Return a domain dog object.</returns>
        public Dog ToDog()
        {
            return new Dog
            {
                Name = Name,
                Color = Color,
                TailLength = TailLength,
                Weight = Weight
            };
        }
    }
}

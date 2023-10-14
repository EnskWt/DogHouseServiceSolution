namespace DogHouseService.Web.Middlewares.PresentationModels
{
    /// <summary>
    /// Represents an error details object for the exception handling middleware.
    /// </summary>
    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public string Message { get; set; } = null!;
    }
}

using DogHouseService.Core.Domain.RepositoryContracts;
using DogHouseService.Core.ServiceContracts;
using DogHouseService.Core.Services;
using DogHouseService.Infrastructure.DatabaseContext;
using DogHouseService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace DogHouseService.Web.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(); // Add controllers to the DI container.

            // Add rate limiter for 429 responses if request limit is exceeded.
            services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("10RequestsLimiter", limiterOptions =>
                {
                    limiterOptions.PermitLimit = 10;
                    limiterOptions.Window = TimeSpan.FromSeconds(1);
                });
                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = 429;
                    await Task.CompletedTask;
                };
            });

            services.AddScoped<IDogsRepository, DogsRepository>(); // Add DogsRepository to the DI container.

            services.AddScoped<IDogsGetterService, DogsGetterService>(); // Add DogsGetterService to the DI container.
            services.AddScoped<IDogsAdderService, DogsAdderService>(); // Add DogsAdderService to the DI container.

            // Add DbContext to the DI container.
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}

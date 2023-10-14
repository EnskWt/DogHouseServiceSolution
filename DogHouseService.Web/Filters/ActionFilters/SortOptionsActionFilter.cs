using DogHouseService.Core.DataTransferObjects.DogObjects;
using DogHouseService.Core.Domain.Models;
using DogHouseService.Core.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DogHouseService.Web.Filters.ActionFilters
{
    /// <summary>
    /// Validate the sort options. If sortAttribute is not a valid sort option, then set it to the default sort option.
    /// </summary>
    public class SortOptionsActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // before logic

            var sortAttributeOptions = SortOptionsHelper.GetSortAttributeOptions();

            if (context.ActionArguments.ContainsKey("sortAttribute"))
            {
                var argumentSortAttribute = (string)context.ActionArguments["sortAttribute"]!;
                if (!sortAttributeOptions.Any(o => o.ToLower() == argumentSortAttribute.ToLower()))
                {
                    context.ActionArguments["sortAttribute"] = typeof(DogResponse).GetProperty(nameof(DogResponse.Name))?.Name;
                }
            }

            await next();

            // after logic
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using TechnologyProvider.Cqrs.Core;

namespace TechnologyProvider.Api.Infrastructure.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsNotFound)
            {
                return new NotFoundObjectResult(result.GetFailure());
            }

            return new BadRequestObjectResult(result.GetFailure());
        }
    }
}

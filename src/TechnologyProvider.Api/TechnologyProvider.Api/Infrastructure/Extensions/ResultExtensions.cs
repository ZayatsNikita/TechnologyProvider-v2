namespace TechnologyProvider.Api.Infrastructure.Extensions
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using TechnologyProvider.Cqrs.Core;

    /// <summary>
    /// Extension methods for <see cref="Result{T}"/>.
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Extension methods that translate the result of the handler execution to IActionResult.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="result">Result object.</param>
        /// <returns>IActionResult object.</returns>
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            ArgumentNullException.ThrowIfNull(result, nameof(result));

            if (result.IsNotFound)
            {
                return new NotFoundObjectResult(result.GetFailure());
            }

            return new BadRequestObjectResult(result.GetFailure());
        }
    }
}

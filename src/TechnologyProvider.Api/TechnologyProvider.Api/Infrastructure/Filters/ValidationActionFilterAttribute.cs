using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechnologyProvider.Api.Models;

namespace TechnologyProvider.Api.Infrastructure.Filters
{
    public sealed class ValidationActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var modelStateErrors = context.ModelState
                    .Where(x => x.Value is not null && x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage))
                    .ToArray();

                var response = new List<ValidationErrorModel>();

                foreach (var error in modelStateErrors)
                {
                    foreach (var subError in error.Value)
                    {
                        var errorModel = new ValidationErrorModel
                        {
                            FieldName = error.Key,
                            Message = subError,
                        };

                        response.Add(errorModel);
                    }
                }

                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}

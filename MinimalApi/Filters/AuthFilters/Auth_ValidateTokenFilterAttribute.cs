using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MinimalApi.Filters.AuthFilters
{
    public class Auth_ValidateTokenFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var token = context.ActionArguments["token"] as string;

            if (token == null) {
                context.ModelState.AddModelError("message", "Token is required");
                var problemsDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                context.Result = new BadRequestObjectResult(problemsDetails);
                return;
            }

            string regexPattern = @"^[A-Za-z0-9-_]+\.[A-Za-z0-9-_]+\.[A-Za-z0-9-_]*$";
            if (token != null && !Regex.IsMatch(token, regexPattern)) {
                context.ModelState.AddModelError("message", "Token is invalid.");
                var problemsDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                context.Result = new BadRequestObjectResult(problemsDetails);
                return;
            }
            await next();
		}
    }
}
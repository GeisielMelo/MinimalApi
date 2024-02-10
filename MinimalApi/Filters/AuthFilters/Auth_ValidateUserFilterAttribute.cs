using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MinimalApi.Domain.Models;
using MinimalApi.Infrastructure.Repositories;

namespace MinimalApi.Filters.AuthFilters
{
    public class Auth_ValidateUserFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userService = context.HttpContext.RequestServices.GetService(typeof(UserRepository)) as UserRepository;
                if (userService == null) {
                    context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                    return;
                }

            if (context.ActionArguments.TryGetValue("user", out var userObj) && userObj is User user) {
                
                var data = await userService.GetByEmail(user.Email);

                if (data == null) {
                    context.ModelState.AddModelError("message", "User not found.");
                    var problemsDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound,
                    };
                    context.Result = new NotFoundObjectResult(problemsDetails);
                    return;
                }

                if (data != null) {
                    if (data.Password != user.Password) {
                        context.ModelState.AddModelError("message", "Password is incorrect.");
                        var problemsDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status401Unauthorized,
                        };
                        context.Result = new UnauthorizedObjectResult(problemsDetails);
                        return;
                    }
                }
            }
            await next();
        }
    }
}
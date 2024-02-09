using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MinimalApi.Domain.Models;
using MinimalApi.Infrastructure.Services;

namespace MinimalApi.Filters.UserFilters
{
    public class User_ValidateRegisteredEmailFilterAttribute : ActionFilterAttribute
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
                
                if (data != null) {
                    context.ModelState.AddModelError("Email", "Email already registered");
                    var problemsDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemsDetails);
                    return;
                }
            }
            await next();
        }
    }
}
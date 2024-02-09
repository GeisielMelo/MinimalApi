using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MinimalApi.Infrastructure.Services;

namespace MinimalApi.Filters.UserFilters
{
    public class User_ValidateUserIdFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var id = context.ActionArguments["id"] as string;

            if (id == null) {
                context.ModelState.AddModelError("message", "Id is required.");
                var problemsDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                context.Result = new BadRequestObjectResult(problemsDetails);
                return;
            }
            else if (id.Length != 24) {
                context.ModelState.AddModelError("message", "Invalid id.");
                var problemsDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                context.Result = new BadRequestObjectResult(problemsDetails);
                return;
            }
            else{
                var userService = context.HttpContext.RequestServices.GetService(typeof(UserRepository)) as UserRepository;
                if (userService == null) {
                    context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                    return;
                }
                var user = await userService.GetUserById(id);
                if (user == null)
                {
                    context.ModelState.AddModelError("message", "User not found.");
                    var problemsDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound,
                    };
                    context.Result = new NotFoundObjectResult(problemsDetails);
                    return;
                }
            }
            await next();
        }
    }
}

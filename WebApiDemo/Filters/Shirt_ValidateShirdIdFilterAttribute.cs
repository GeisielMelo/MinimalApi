using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiDemo.Models.Repositories;

namespace WebApiDemo.Filters
{
    public class Shirt_ValidateShirtIdFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var Id = context.ActionArguments["id"] as int?;
            if (Id.HasValue) {
                if(Id.Value <=0) {
                    context.ModelState.AddModelError("Id", "Invalid shirt id.");

                    var problemsDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };

                    context.Result = new BadRequestObjectResult(problemsDetails);
                }
                else if (!ShirtRepository.ShirtExists(Id.Value)) {
                    context.ModelState.AddModelError("Id", "Shirt doesn't exist.");

                    var problemsDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    };

                    context.Result = new NotFoundObjectResult(problemsDetails);
                }
            }
        }
    }
}
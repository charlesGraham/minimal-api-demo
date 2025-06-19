using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiDemo.Filters
{
    public class ValidateShirtIdFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var shirtId = context.ActionArguments["id"] as int?;

            if (!shirtId.HasValue)
            {
                context.ModelState.AddModelError("id", "ID is required");

                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "ID is required",
                    Instance = context.HttpContext.Request.Path,
                };

                context.Result = new BadRequestObjectResult(problemDetails);
                return;
            }
            else if (shirtId.Value <= 0)
            {
                context.ModelState.AddModelError("id", "ID is invalid");

                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "ID is invalid",
                    Instance = context.HttpContext.Request.Path,
                };

                context.Result = new BadRequestObjectResult(problemDetails);
                return;
            }
            // else if (!_shirtRepo.ShirtExists(shirtId.Value))
            // {
            //     context.ModelState.AddModelError("id", "Shirt not found");

            //     var problemDetails = new ValidationProblemDetails(context.ModelState)
            //     {
            //         Status = StatusCodes.Status404NotFound,
            //         Title = "Not Found",
            //         Detail = "Shirt not found",
            //         Instance = context.HttpContext.Request.Path,
            //     };

            //     context.Result = new NotFoundObjectResult(problemDetails);
            //     return;
            // }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiDemo.Models.Repositories;

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
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }
            else if (shirtId.Value <= 0)
            {
                context.ModelState.AddModelError("id", "ID is invalid");
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }
            else if (!ShirtRepo.ShirtExists(shirtId.Value))
            {
                context.ModelState.AddModelError("id", "Shirt not found");
                context.Result = new NotFoundObjectResult(context.ModelState);
                return;
            }
        }
    }
}

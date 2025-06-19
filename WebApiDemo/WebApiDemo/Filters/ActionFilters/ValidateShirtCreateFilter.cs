using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiDemo.Data;
using WebApiDemo.Models;

namespace WebApiDemo.Filters.ActionFilters
{
    public class ValidateShirtCreateFilter : ActionFilterAttribute
    {
        private readonly ApplicationDbContext db;

        public ValidateShirtCreateFilter(ApplicationDbContext db)
        {
            this.db = db;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var shirt = context.ActionArguments["shirt"] as Shirt;

            if (shirt == null)
            {
                context.ModelState.AddModelError("Shirt", "Shirt object is null.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
            else
            {
                var existingShirt = db.Shirts.FirstOrDefault(x =>
                    !string.IsNullOrWhiteSpace(shirt.Brand)
                    && !string.IsNullOrWhiteSpace(x.Brand)
                    && x.Brand.ToLower() == shirt.Brand.ToLower()
                    && !string.IsNullOrWhiteSpace(shirt.Gender)
                    && !string.IsNullOrWhiteSpace(x.Gender)
                    && x.Gender.ToLower() == shirt.Gender.ToLower()
                    && !string.IsNullOrWhiteSpace(shirt.Color)
                    && !string.IsNullOrWhiteSpace(x.Color)
                    && x.Color.ToLower() == shirt.Color.ToLower()
                    && shirt.Size == x.Size
                );

                if (existingShirt != null)
                {
                    context.ModelState.AddModelError("Shirt", "Shirt already exists.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }
        }
    }
}

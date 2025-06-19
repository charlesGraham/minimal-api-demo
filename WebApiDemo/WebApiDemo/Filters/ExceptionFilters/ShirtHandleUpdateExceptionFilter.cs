using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiDemo.Data;
using WebApiDemo.Models.Repositories;

namespace WebApiDemo.Filters.ExceptionFilters
{
    public class ShirtHandleUpdateExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ApplicationDbContext _db;

        public ShirtHandleUpdateExceptionFilter(ApplicationDbContext db)
        {
            _db = db;
        }

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var strShirtId = context.RouteData.Values["id"] as string;
            if (int.TryParse(strShirtId, out int shirtId))
            {
                if (_db.Shirts.FirstOrDefault(x => x.Id == shirtId) == null)
                {
                    context.ModelState.AddModelError("ShirtId", "Shirt doesn't exist anymore.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound,
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                }
            }
        }
    }
}

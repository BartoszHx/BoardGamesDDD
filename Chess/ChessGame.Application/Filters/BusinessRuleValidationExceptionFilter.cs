using ChessGame.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChessGame.Application.Filters
{
    public class BusinessRuleValidationExceptionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is BusinessRuleValidationException exception)
            {
                var errors = new Dictionary<string, string[]>
                {
                    { "BusinessRule", new string[] { exception.ToString() } }
                };

                context.Result = new ObjectResult(new HttpValidationProblemDetails(errors))
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };

                /*
                context.Result = new ObjectResult(exception.ToString())
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
                */
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }
}

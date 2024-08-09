using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChessGame.Application.Filters
{
    public class HandlerValidationExceptionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ValidationException exception)
            {
                var errors = exception.Errors.ToDictionary(k => k.PropertyName, e => new string[] { e.ErrorMessage });

                context.Result = new ObjectResult(new HttpValidationProblemDetails(errors))
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };

                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }
}

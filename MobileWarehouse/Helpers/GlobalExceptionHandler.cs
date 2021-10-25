using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace MobileWarehouse.Helpers
{
    public class GlobalExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                var exceptionSource = context.Exception.Source;
                var exceptionStack = context.Exception.StackTrace;
                var exceptionMessage = context.Exception.Message;

                context.ExceptionHandled = false;
                context.Result = new JsonResult("Something was wrong!");
                context.HttpContext.Response.StatusCode = 400;

                Log.Error($"{nameof(GlobalExceptionHandler)} | Source - {exceptionSource} | Message - {exceptionMessage} | StackTrace - {exceptionStack}.");
            }
        }
    }
}

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
                context.ExceptionHandled = false;
                context.Result = new JsonResult("Something was wrong!");
                context.HttpContext.Response.StatusCode = 400;

                Log.Error($"{nameof(GlobalExceptionHandler)} | Source - {context.Exception.Source} | Message - {context.Exception.StackTrace} | StackTrace - {context.Exception.Message}.");
            }
        }
    }
}

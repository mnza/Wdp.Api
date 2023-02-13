using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Wdp.Api.Filters
{
    public class GlobalExceptionsFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                context.Result = new ObjectResult(ResponseModel.Error(context.Exception.Message + "\r\n" +context.Exception.StackTrace));
                Log.Error(context.Exception,"错误信息");
            }
            context.ExceptionHandled = true;
        }
    }
}

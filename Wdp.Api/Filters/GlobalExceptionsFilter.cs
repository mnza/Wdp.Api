using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Wdp.Api.Filters
{
    public class GlobalExceptionsFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                context.Result = new ObjectResult(ResponseModel.Error(context.Exception.Message + "\r\n" +context.Exception.StackTrace));
            }
            context.ExceptionHandled = true;
        }
    }
}

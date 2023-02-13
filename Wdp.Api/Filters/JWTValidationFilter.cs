using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Security.Claims;

namespace Wdp.Api.Filters
{
    public class JWTValidationFilter : IAsyncActionFilter
    {


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var claimUserName = context.HttpContext.User.FindFirst(ClaimTypes.Name);
            if (claimUserName == null)
            {
                await next();
                return;
            }
            string name = claimUserName!.Value;

            if(name == "ZL" || name == "TQ" || name == "LFF")
            {
                await next();
            }
            else
            {
                var result = new ObjectResult("非法用户");
                result.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = result;
                return;
            }
            

        }
    }
}

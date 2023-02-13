namespace Wdp.Api.Middlewares
{
    public static class TestMiddlewareExtension
    {
        public static IApplicationBuilder UseTestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TestMiddleware>();
        }
    }
}

namespace Wdp.Api.Middlewares
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;

        public TestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("中间件到此一游");
            Console.WriteLine(context.Connection.RemoteIpAddress?.ToString());

            await _next.Invoke(context);
        }
    }
}

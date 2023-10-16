namespace BowlingApp.Middlewares
{
    public class IPWhitelistingMiddleware : IMiddleware
    {
      
        private readonly RequestDelegate _next { get; set; };

        public IPWhitelistingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        private readonly List<string> _allowedIpAddresses = new List<string> { "192.168.1.100", "10.0.0.1" };
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var ipAddress = context.Connection.RemoteIpAddress;
            if (!_allowedIpAddresses.Contains(ipAddress?.ToString()))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access denied.");
                return;
            }
            await  _next(context);
        }
    }
}

using back.net_core.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
    public static class ResponseWrapperExtensions
    {
        public static IApplicationBuilder UseResponseWrapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseWrapperProcessor>();
        }
    }
}

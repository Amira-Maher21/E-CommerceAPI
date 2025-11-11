using E_CommerceAPI.middleware;

namespace E_CommerceAPI.Extentions
{
    public static class CustomGlobalErrorHandlerMiddleware
    {
        public static IApplicationBuilder GlobalErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalErrorHandling>();
        }
    }
}

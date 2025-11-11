using E_CommerceAPI.middleware;

namespace E_CommerceAPI.Extentions
{
    public static class CustomMiddleWare
    {
        public static IApplicationBuilder TransactionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TransactionMiddleware>();
        }
    }
}

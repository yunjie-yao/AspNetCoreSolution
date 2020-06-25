using Microsoft.AspNetCore.Builder;

namespace YangXuAPI.Helpers
{
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder UseResponseTimeMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ResponseTimeMiddleware>();
        }

    }
}

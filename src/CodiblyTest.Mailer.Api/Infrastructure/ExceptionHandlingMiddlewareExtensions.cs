using Microsoft.AspNetCore.Builder;

namespace CodiblyTest.Mailer.Api.Infrastructure
{
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            return app;
        }
    }
}
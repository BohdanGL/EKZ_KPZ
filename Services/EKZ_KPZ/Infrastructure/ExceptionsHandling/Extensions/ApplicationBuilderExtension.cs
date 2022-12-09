using Microsoft.AspNetCore.Builder;

namespace EKZ_KPZ.Infrastructure.ExceptionsHandling.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}

using Forum.Api.Common.Errors;
using Forum.Api.Common.Helpers;
using Forum.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;

namespace Forum.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddRazorPages();
        services.AddSingleton<ProblemDetailsFactory, ForumProblemDetailsFactory>();
        services.AddSingleton<IUserIdProvider, TokenUserIdProvider>();
        services.AddSwaggerGen();

        services.AddMappings();
        return services;
    }

    public static IServiceCollection AddMyCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }
}
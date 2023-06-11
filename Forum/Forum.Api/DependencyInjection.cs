using Forum.Api.Common.Errors;
using Forum.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;

namespace Forum.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, ForumProblemDetailsFactory>();
        services.AddMappings();
        return services;
    }
}
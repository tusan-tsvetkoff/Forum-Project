using FluentValidation;
using Forum.Application.Authentication.Common;
using Forum.Application.Common.Behaviors;
using Forum.Application.Common.Interfaces.Services;
using Forum.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Forum.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IPostStatisticsService, PostStatisticsService>();
            services.AddScoped<IUserStatisticsServices, UserStatisticsServices>();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
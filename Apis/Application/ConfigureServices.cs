using Application.Commons.Behaviors;
using Application.Interfaces;
using Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            #region services
            services.AddScoped<ITestAssessmentService, TestAssessmentService>();
            #endregion services
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });
            return services;
        }
    }
}
// TODO: Add caching service(memory cache or redis cache). Add excel service.

// TODO: DO FIRST Do authentication feature which has send mail when registration, role, set up policies.
using System.Reflection;
using Application.Common.Behaviours;
using Application.Common.Identity;
using Application.Common.Interfaces;
using Application.Common.Services;
using Application.Data.Persistence;
using Application.Feature.RecipientAddress.Commands.CreateByFile;
using AutoMapper;
using FluentValidation;
using Hangfire;
using Hangfire.MemoryStorage;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            return services;
        }
        
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("HackatonAppDb"));
                // Подключение к планировщику задач - Hangfire.
                services.AddHangfire(hangfireConfiguration =>
                {
                    hangfireConfiguration
                        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                        .UseSimpleAssemblyNameTypeSerializer()
                        .UseRecommendedSerializerSettings()
                        .UseMemoryStorage();
                    hangfireConfiguration.UseSerializerSettings(new JsonSerializerSettings
                        {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
                });
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly("WebUI")));
                // Подключение к планировщику задач - Hangfire.
                services.AddHangfire(hangfireConfiguration =>
                {
                    hangfireConfiguration
                        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                        .UseSimpleAssemblyNameTypeSerializer()
                        .UseRecommendedSerializerSettings()
                        .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"));
                    hangfireConfiguration.UseSerializerSettings(new JsonSerializerSettings
                        {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
                });
            }
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<RecordAnaliseJob>();

            // services.AddScoped(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();
            
            
            return services;
        }

    }
}

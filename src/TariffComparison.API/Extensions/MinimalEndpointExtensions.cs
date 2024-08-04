﻿using Microsoft.Extensions.DependencyInjection.Extensions;
using TariffComparison.API.Abstractions;

namespace TariffComparison.API.Extensions
{
    public static class MinimalEndpointExtensions
    {
        public static IServiceCollection AddMinimalEndpoints(this IServiceCollection services)
        {
            var assembly = typeof(Program).Assembly;

            var serviceDescriptors = assembly
                .DefinedTypes
                .Where(type => !type.IsAbstract &&
                               !type.IsInterface &&
                               type.IsAssignableTo(typeof(IMinimalEndpoint)))
                .Select(type => ServiceDescriptor.Transient(typeof(IMinimalEndpoint), type));

            services.TryAddEnumerable(serviceDescriptors);

            return services;
        }

        public static IApplicationBuilder RegisterMinimalEndpoints(this WebApplication app)
        {
            var endpoints = app.Services.GetRequiredService<IEnumerable<IMinimalEndpoint>>();

            foreach (var endpoint in endpoints)
            {
                endpoint.MapRoutes(app);
            }

            return app;
        }
    }
}
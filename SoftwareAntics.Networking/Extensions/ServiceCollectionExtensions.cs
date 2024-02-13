// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace SoftwareAntics.Networking.Extensions;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoftwareAntics.Networking.Clients;
using SoftwareAntics.Networking.Invocation;
using SoftwareAntics.Networking.Servers;

[ExcludeFromCodeCoverage(Justification = "Extensions")]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddClient<TService, TImplementation>(this IServiceCollection services, IConfiguration configuration)
        where TService : class
        where TImplementation : Client, TService
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        services.AddLogging();

        services.AddOptions<ClientOptions>()
            .Bind(configuration.GetRequiredSection(nameof(ClientOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<ITcpClientFactory, TcpClientFactory>();
        services.AddSingleton<TService, TImplementation>();

        return services;
    }

    public static IServiceCollection AddServer<TService, TImplementation>(this IServiceCollection services, IConfiguration configuration)
            where TService : class
            where TImplementation : Server, TService
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        services.AddLogging();

        services.AddOptions<ServerOptions>()
            .Bind(configuration.GetRequiredSection(nameof(ServerOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<ITcpListenerFactory, TcpListenerFactory>();
        services.AddSingleton<TService, TImplementation>();

        return services;
    }
}

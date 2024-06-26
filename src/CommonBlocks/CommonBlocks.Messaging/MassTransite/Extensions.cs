﻿using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CommonBlocks.Messaging.MassTransite;

public static class Extensions
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();
            
            if(assembly != null) config.AddConsumers(assembly);

            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBroker:host"]!), host =>
                {
                    host.Username(configuration["MessageBroker:userName"]!);
                    host.Password(configuration["MessageBroker:password"]!);
                });

                configurator.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}

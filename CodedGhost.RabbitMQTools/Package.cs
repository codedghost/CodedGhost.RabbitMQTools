using System;
using CodedGhost.RabbitMQTools.Abstractions;
using CodedGhost.RabbitMQTools.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CodedGhost.RabbitMQTools
{
    public static class Package
    {
        public static IServiceCollection AddConnectionRabbitServices(this IServiceCollection services)
        {
            services.AddSingleton<ICodedRabbitConnectionFactory, CodedRabbitConnectionFactory>();

            return services;
        }

        public static IServiceCollection AddRabbitPublisherService(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMessagePublisher, RabbitMessagePublisher>();

            return services;
        }

        public static void InitialiseConsumers(this IServiceProvider serviceProvider)
        {
            var consumers = serviceProvider.GetServices<IRabbitConsumer>();
        }
    }
}
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Imtudou.Core.Cache;
using Imtudou.Core.Cache.StackExchangeRedis;

namespace Imtudou.Core.StackExchangeRedis
{
    public static class StackExchangeRedisExtension
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            services.AddOptions<RedisOptions>()
                .Configure(configuration.Bind)
                .ValidateDataAnnotations();
            services.AddSingleton<IRedisCache, RedisCache>();
            return services;
        }

        public static IServiceCollection AddRedis(this IServiceCollection services, Action<RedisOptions> configureOptions)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            services.Configure(configureOptions);
            services.AddSingleton<IRedisCache, RedisCache>();
            return services;
        }
    }
}

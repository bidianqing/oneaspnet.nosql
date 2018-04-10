using Microsoft.Extensions.DependencyInjection;
using System;

namespace OneAspNet.NoSql.Redis
{
    public static class RedisServiceCollectionExtensions
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, Action<RedisOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddOptions();
            services.Configure(setupAction);
            services.AddSingleton(typeof(RedisCache));

            return services;
        }
    }
}

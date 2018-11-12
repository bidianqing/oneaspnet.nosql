using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;

namespace OneAspNet.NoSql.Redis
{
    public class RedisCache
    {
        private ConnectionMultiplexer _connection;
        public IDatabase Database;
        private readonly RedisOptions _options;
        private static object _obj = new object();
        public RedisCache(IOptions<RedisOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            _options = optionsAccessor.Value;

            if (_connection == null)
            {
                lock (_obj)
                {
                    if (_connection == null)
                    {
                        _connection = ConnectionMultiplexer.Connect(_options.Configuration);
                        Database = _connection.GetDatabase();
                    }
                }
            }

        }
    }
}

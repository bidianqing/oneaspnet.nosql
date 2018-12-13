using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading;

namespace OneAspNet.NoSql.Redis
{
    public class RedisCache
    {
        private ConnectionMultiplexer _connection;
        private readonly RedisOptions _options;
        private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(initialCount: 1, maxCount: 1);
        private Dictionary<int, IDatabase> _databases = new Dictionary<int, IDatabase>();
        public RedisCache(IOptions<RedisOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            _options = optionsAccessor.Value;

            _connectionLock.Wait();
            try
            {
                if (_connection == null)
                {
                    if (_options.ConfigurationOptions != null)
                    {
                        _connection = ConnectionMultiplexer.Connect(_options.ConfigurationOptions);
                    }
                    else
                    {
                        _connection = ConnectionMultiplexer.Connect(_options.Configuration);
                    }

                    for (int i = 0; i < _options.DatabaseNumber; i++)
                    {
                        _databases.Add(i, _connection.GetDatabase(i));
                    }
                }
            }
            finally
            {
                _connectionLock.Release();
            }

        }


        public IDatabase GetDatabase(int db = 0)
        {
            if (_databases.TryGetValue(db, out IDatabase database))
            {
                return database;
            }

            return _connection.GetDatabase();
        }
    }
}

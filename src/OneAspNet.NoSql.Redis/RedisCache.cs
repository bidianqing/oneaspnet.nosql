using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace OneAspNet.NoSql.Redis
{
    public class RedisCache
    {
        private ConnectionMultiplexer _connection;
        private readonly RedisOptions _options;
        private static object _obj = new object();
        private Dictionary<int, IDatabase> _databases = new Dictionary<int, IDatabase>();
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

                        for (int i = 0; i < _options.DatabaseNumber; i++)
                        {
                            _databases.Add(i, _connection.GetDatabase(i));
                        }
                    }
                }
            }
        }


        public IDatabase GetDatabase(int db)
        {
            if (_databases.TryGetValue(db, out IDatabase database))
            {
                return database;
            }

            return _connection.GetDatabase();
        }
    }
}

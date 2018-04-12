using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace OneAspNet.NoSql.Mongo
{
    public class MongoRepository<T>
    {
        private static IMongoDatabase _mongoDatabase;
        private readonly MongoOptions _options;
        private static object _obj = new object();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IMongoCollection<T>> _mongoCollections = new ConcurrentDictionary<RuntimeTypeHandle, IMongoCollection<T>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> _collectionNames = new ConcurrentDictionary<RuntimeTypeHandle, string>();

        public MongoRepository(IOptions<MongoOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            _options = optionsAccessor.Value;

            if (_mongoDatabase == null)
            {
                lock (_obj)
                {
                    if (_mongoDatabase == null)
                    {
                        var _databaseName = MongoUrl.Create(_options.ConnectionString).DatabaseName;

                        _mongoDatabase = new MongoClient(_options.ConnectionString).GetDatabase(_databaseName);
                    }
                }
            }
        }

        public IMongoCollection<T> Collection
        {
            get
            {
                if (_mongoCollections.TryGetValue(typeof(T).TypeHandle, out IMongoCollection<T> collection))
                {
                    return collection;
                }

                string collectionName = this.GetCollectionName(typeof(T));

                collection = _mongoDatabase.GetCollection<T>(collectionName);
                _mongoCollections.TryAdd(typeof(T).TypeHandle, collection);

                return collection;
            }
        }

        private string GetCollectionName(Type type)
        {
            if (_collectionNames.TryGetValue(type.TypeHandle, out string name)) return name;

            var collectionAttr = type
#if NETSTANDARD1_3
                    .GetTypeInfo()
#endif
                    .GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "CollectionAttribute") as CollectionAttribute;
            if (collectionAttr != null)
            {
                name = collectionAttr.Name;
            }
            else
            {
                name = $"{type.Name}Collection";
            }
            _collectionNames.TryAdd(type.TypeHandle, name);

            return name;
        }

    }
}

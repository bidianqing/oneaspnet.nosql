using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneAspNet.NoSql.Redis
{
    public class RedisCache
    {
        private static ConnectionMultiplexer _connection;
        private static IDatabase _cache;
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
                        _cache = _connection.GetDatabase(_options.Database);
                    }
                }
            }

        }


        #region String
        public bool StringSet(string key, string value)
        {
            return StringSet(key, value, null);
        }
        public bool StringSet(string key, string value, TimeSpan? expiry)
        {
            return _cache.StringSet(key, value, expiry);
        }
        public async Task<bool> StringSetAsync(string key, string value)
        {
            return await StringSetAsync(key, value, null);
        }
        public async Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry)
        {
            return await _cache.StringSetAsync(key, value, expiry);
        }
        public string StringGet(string key)
        {
            return _cache.StringGet(key);
        }
        public async Task<string> StringGetAsync(string key)
        {
            return await _cache.StringGetAsync(key);
        }
        public long StringIncrement(string key)
        {
            return _cache.StringIncrement(key);
        }
        public async Task<long> StringIncrementAsync(string key)
        {
            return await _cache.StringIncrementAsync(key);
        }
        #endregion

        #region Hash
        public bool HashSet(string key, string field, string value)
        {
            return _cache.HashSet(key, field, value);
        }
        public async Task<bool> HashSetAsync(string key, string field, string value)
        {
            return await _cache.HashSetAsync(key, field, value);
        }
        public string HashGet(string key, string field)
        {
            return _cache.HashGet(key, field);
        }
        public async Task<string> HashGetAsync(string key, string field)
        {
            return await _cache.HashGetAsync(key, field);
        }
        public long HashIncrement(string key, string field)
        {
            return _cache.HashIncrement(key, field);
        }
        public async Task<long> HashIncrementAsync(string key, string field)
        {
            return await _cache.HashIncrementAsync(key, field);
        }

        #endregion

        #region List
        public long ListRightPush(string key, string value)
        {
            return _cache.ListRightPush(key, value);
        }
        public async Task<long> ListRightPushAsync(string key, string value)
        {
            return await _cache.ListRightPushAsync(key, value);
        }
        public long ListLeftPush(string key, string value)
        {
            return _cache.ListLeftPush(key, value);
        }
        public async Task<long> ListLeftPushAsync(string key, string value)
        {
            return await _cache.ListLeftPushAsync(key, value);
        }
        public List<string> ListRange(string key)
        {
            List<string> list = new List<string>();
            var values = _cache.ListRange(key);
            foreach (var item in values)
            {
                list.Add(item);
            }
            return list;
        }
        public async Task<List<string>> ListRangeAsync(string key)
        {
            List<string> list = new List<string>();
            var values = await _cache.ListRangeAsync(key);
            foreach (var item in values)
            {
                list.Add(item);
            }
            return list;
        }
        #endregion

        #region Set
        public bool SetAdd(string key, string value)
        {
            return _cache.SetAdd(key, value);
        }
        public async Task<bool> SetAddAsync(string key, string value)
        {
            return await _cache.SetAddAsync(key, value);
        }
        public List<string> SetMembers(string key)
        {
            List<string> list = new List<string>();
            var values = _cache.SetMembers(key);
            foreach (var item in values)
            {
                list.Add(item);
            }
            return list;
        }
        public async Task<List<string>> SetMembersAsync(string key)
        {
            List<string> list = new List<string>();
            var values = await _cache.SetMembersAsync(key);
            foreach (var item in values)
            {
                list.Add(item);
            }
            return list;
        }
        #endregion

        #region ZSet
        public bool SortedSetAdd(string key, string value, double score)
        {
            return _cache.SortedSetAdd(key, value, score);
        }
        public async Task<bool> SortedSetAddAsync(string key, string value, double score)
        {
            return await _cache.SortedSetAddAsync(key, value, score);
        }
        public List<string> SortedSetRangeByRank(string key)
        {
            List<string> list = new List<string>();
            var values = _cache.SortedSetRangeByRank(key);
            foreach (var item in values)
            {
                list.Add(item);
            }
            return list;
        }
        public async Task<List<string>> SortedSetRangeByRankAsync(string key)
        {
            List<string> list = new List<string>();
            var values = await _cache.SortedSetRangeByRankAsync(key);
            foreach (var item in values)
            {
                list.Add(item);
            }
            return list;
        }
        #endregion

        #region Keys operation 
        public static List<string> Keys(string pattern = "")
        {
            List<string> list = new List<string>();
            var keys = _connection.GetServer(_connection.GetEndPoints()[0]).Keys(0, pattern);
            foreach (var item in keys)
            {
                list.Add(item);
            }
            return list;
        }
        public bool KeyDelete(string key)
        {
            return _cache.KeyDelete(key);
        }
        public async Task<bool> KeyDeleteAsync(string key)
        {
            return await _cache.KeyDeleteAsync(key);
        }
        #endregion

    }
}

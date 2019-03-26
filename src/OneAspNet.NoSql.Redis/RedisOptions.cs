using StackExchange.Redis;

namespace OneAspNet.NoSql.Redis
{
    public class RedisOptions
    {
        /// <summary>
        /// The configuration used to connect to Redis.
        /// </summary>
        public string Configuration { get; set; }

        /// <summary>
        /// The configuration used to connect to Redis.
        /// This is preferred over Configuration.
        /// </summary>
        public ConfigurationOptions ConfigurationOptions { get; set; }

        /// <summary>
        /// The number of databases,default value is 16
        /// </summary>
        public int NumOfDatabases { get; set; } = 16;
    }
}

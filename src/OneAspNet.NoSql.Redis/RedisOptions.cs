namespace OneAspNet.NoSql.Redis
{
    public class RedisOptions
    {
        public string Configuration { get; set; }
        public int DatabaseNumber { get; set; } = 16;
    }
}

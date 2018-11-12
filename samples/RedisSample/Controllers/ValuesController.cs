using Microsoft.AspNetCore.Mvc;
using OneAspNet.NoSql.Redis;
using StackExchange.Redis;
using System.Collections.Generic;

namespace RedisSample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IDatabase _redis;
        public ValuesController(RedisCache redisCache)
        {
            _redis = redisCache.GetDatabase(0);
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _redis.StringSet("name", "bidianqing");

            var value = _redis.StringGet("name");
            return new string[] { value };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

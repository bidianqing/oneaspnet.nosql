using Microsoft.AspNetCore.Mvc;
using OneAspNet.NoSql.Redis;
using System.Collections.Generic;

namespace RedisSample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly RedisCache _redisCache;
        public ValuesController(RedisCache redisCache)
        {
            _redisCache = redisCache;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _redisCache.StringSet("name", "bidianqing");

            var value = _redisCache.StringGet("name");
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

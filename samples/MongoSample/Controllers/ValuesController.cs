using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OneAspNet.NoSql.Mongo;
using System.Collections.Generic;

namespace MongoSample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly MongoRepository<User> _userMongoRepository;
        public ValuesController(MongoRepository<User> userMongoRepository)
        {
            _userMongoRepository = userMongoRepository;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _userMongoRepository.Collection.InsertOne(new User
            {
                Email = "bidianqing@qq.com"
            });

            var userList = _userMongoRepository.Collection.Find(u => true).ToList();

            return new string[] { "value1", "value2" };
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

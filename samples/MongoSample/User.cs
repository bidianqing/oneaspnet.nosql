using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OneAspNet.NoSql.Mongo;

namespace MongoSample
{
    [Collection("helloUser")]
    public class User
    {
        public object _id { get; set; } = ObjectId.GenerateNewId();
        [BsonElement("email")]
        public string Email { get; set; }
    }
}

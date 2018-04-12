using System;

namespace OneAspNet.NoSql.Mongo
{
    public class CollectionAttribute : Attribute
    {
        public CollectionAttribute(string collectionName)
        {
            this.Name = collectionName;
        }
        public string Name { get; set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Food_WebAppMongoDB.Entities
{
    public class Foods
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string FoodName { get; set; }
        public string Company { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}

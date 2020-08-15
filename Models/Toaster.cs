using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ToasterApi.Models
{
    public class Toaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        [JsonProperty("Name")]
        public string ToasterName { get; set; }

        public bool On { get; set; }

        public int Heat { get; set; }

        public int Time { get; set; }
    }
}
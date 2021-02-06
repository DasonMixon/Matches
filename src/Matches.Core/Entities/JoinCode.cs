using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Matches.Core.Entities
{
    public class JoinCode
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }
    }
}

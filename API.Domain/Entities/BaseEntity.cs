using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Domain.Enum;

namespace API.Domain.Entities
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        public bool Deleted { get; set; }

        public string Slug { get; set; }

        [BsonElement("publishDate")]
        public DateTime PublishDate { get; protected set; }

        [BsonElement("status")]
        public Status Status { get; protected set; }
    }
}

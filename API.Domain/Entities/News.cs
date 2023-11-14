using API.Domain.Enum;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Domain.Entities
{
    public class News : BaseEntity
    {
        public News(string hat, string title, string text, string author, string img, string link, Status status)
        {
            Hat = hat;
            Title = title;
            Text = text;
            Author = author;
            Img = img;
            PublishDate = DateTime.Now;
            Link = link;
            Status = status;
        }

        public Status ChangeStatus(Status status)
        {
            switch (status)
            {
                case Status.Active:
                    Status = Status.Active;
                    break;
                case Status.Inactive:
                    Status = Status.Inactive;
                    break;
                case Status.Draft:
                    Status = Status.Draft;
                    break;
            }

            return status;
        }

        [BsonElement("hat")]
        public string Hat { get; private set; }

        [BsonElement("title")]
        public string Title { get; private set; }

        [BsonElement("text")]
        public string Text { get; private set; }

        [BsonElement("author")]
        public string Author { get; private set; }

        [BsonElement("img")]
        public string Img { get; private set; }

        [BsonElement("link")]
        public string Link { get; private set; }

        [BsonElement("publishDate")]
        public DateTime PublishDate { get; private set; }

        [BsonElement("status")]
        public Status Status { get; private set; }

    }
}

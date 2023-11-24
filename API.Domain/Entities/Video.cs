using System;
using API.Domain.Enum;
using API.Infra.Util;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Domain.Entities
{
	public class Video : BaseEntity
	{
		

        public Video(string hat, string title, string author, string thumbnail, string urlVideo, Status status)
        {
            Hat = hat;
            Title = title;
            Author = author;
            UrlVideo = urlVideo;
            PublishDate = DateTime.Now;
            Status = status;
            Slug = Helper.GenerateSlug(title);
            Thumbnail = thumbnail;
            

            ValidaEntity();
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

        [BsonElement("author")]
        public string Author { get; private set; }

        [BsonElement("thumbnail")]
        public string Thumbnail { get; private set; }

        [BsonElement("urlVideo")]
        public string UrlVideo { get; private set; }


        public void ValidaEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Title, "O título não pode estar vazio!");
            AssertionConcern.AssertArgumentNotEmpty(Hat, "O chapéu não pode estar vazio!");

            AssertionConcern.AssertArgumentLength(Title, 90, "O título deve ter até 90 caracteres!");
            AssertionConcern.AssertArgumentLength(Hat, 40, "O chapéu deve ter até 40 caracteres!");
        }
    }
}


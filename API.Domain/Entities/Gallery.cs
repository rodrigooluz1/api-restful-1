using System;
using System.Collections.Generic;
using API.Domain.Enum;
using API.Infra.Util;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Domain.Entities
{
	public class Gallery : BaseEntity
	{


        public Gallery(string title, string legend, string tags, string author, string thumbnail, IList<string> galleryImages, Status status)
        {

            Title = title;
            Author = author;
            Legend = legend;
            Tags = tags;
            GalleryImages = galleryImages;
            Slug = Helper.GenerateSlug(title);
            Thumbnail = thumbnail;
            PublishDate = DateTime.Now;

            ValidaEntity();
        }

        [BsonElement("title")]
        public string Title { get; private set; }

        [BsonElement("legend")]
        public string Legend { get; private set; }

        [BsonElement("tags")]
        public string Tags { get; private set; }

        [BsonElement("author")]
        public string Author { get; private set; }

        [BsonElement("thumbnail")]
        public string Thumbnail { get; private set; }

        [BsonElement("slug")]
        public string Slug { get; private set; }

        [BsonElement("galleryImages")]
        public IList<string> GalleryImages { get; private set; }


        public void ValidaEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Title, "O título não pode estar vazio!");
            AssertionConcern.AssertArgumentNotEmpty(Legend, "A legenda pode estar vazia!");

            AssertionConcern.AssertArgumentLength(Title, 90, "O título deve ter até 90 caracteres!");
            AssertionConcern.AssertArgumentLength(Legend, 40, "A legenda deve ter até 40 caracteres!");
        }




}
}


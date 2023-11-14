using System;
using API.DAL.IRepositories;
using API.Domain.Entities;
using API.Infra;
using MongoDB.Driver;

namespace API.DAL.Repositories
{
	public class MongoRepository<T>: IMongoRepository<T> where T : BaseEntity
	{
        private readonly IMongoCollection<T> _model;

        public MongoRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.connetionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _model = database.GetCollection<T>(typeof(T).Name.ToLower());
        }


		
        public T Create(T news)
        {
            _model.InsertOne(news);
            return news;
        }

        public List<T> Get() => _model.Find(active => true).ToList();

        public T Get(string id) => _model.Find<T>(news => news.ID == id).FirstOrDefault();

        public void Remove(string id) => _model.DeleteOne(news => news.ID == id);

        public void Update(string id, T news) => _model.ReplaceOne(news => news.ID == id, news);

    }
}


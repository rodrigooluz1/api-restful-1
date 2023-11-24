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
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _model = database.GetCollection<T>(typeof(T).Name.ToLower());
        }


		
        public T Create(T news)
        {
            _model.InsertOne(news);
            return news;
        }

        //public List<T> Get() => _model.Find(news => news.Deleted != true).ToList();
        public Result<T> Get(int page, int qtd)
        {
            var result = new Result<T>();
            result.Page = page;
            result.Qtd = qtd;
            var filter = Builders<T>.Filter.Eq(entity => entity.Deleted, false);

            result.Data = _model.Find(filter)
                    .SortByDescending(entity => entity.PublishDate)
                    .Skip((page - 1) * qtd).Limit(qtd).ToList();

            result.Total = _model.CountDocuments(filter);
            result.TotalPages = result.Total / qtd;

            return result;
        }

        public T Get(string id) => _model.Find<T>(news => news.ID == id && news.Deleted != true).FirstOrDefault();

        public T GetBySlug(string slug) => _model.Find<T>(news => news.Slug == slug && news.Deleted != true).FirstOrDefault();

        public void Update(string id, T news) => _model.ReplaceOne(news => news.ID == id, news);

        public void Remove(string id)
        {
            var news = Get(id);
            news.Deleted = true;
            _model.ReplaceOne(news => news.ID == id, news);
            //model.DeleteOne(news => news.ID == id);
        }
    }
}


using System;
using API.DAL.IRepositories;
using API.Domain.Entities;
using API.Domain.ViewModels;
using AutoMapper;

namespace API.Services
{
	public class NewsService : INewsService
	{
        private readonly IMapper _mapper;

        private readonly IMongoRepository<News> _news;

		public NewsService(IMapper mapper, IMongoRepository<News> news) {
            _mapper = mapper;
            _news = news;
        }

        public Result<NewsViewModel> Get(int page, int qtd) => _mapper.Map<Result<NewsViewModel>>(_news.Get(page, qtd));

        public NewsViewModel Get(string id) => _mapper.Map<NewsViewModel>(_news.Get(id));

        public NewsViewModel GetBySlug(string slug) => _mapper.Map<NewsViewModel>(_news.GetBySlug(slug));

        public NewsViewModel Create(NewsViewModel news)
        {
            var entity = new News(news.Hat, news.Title, news.Text, news.Author, news.Img, news.Status);
            _news.Create(entity);

            return Get(entity.ID);
        }

        public void Update(string id, NewsViewModel newsIn)
        {
            _news.Update(id, _mapper.Map<News>(newsIn));
        }

        public void Remove(string id) => _news.Remove(id);

        
    }
}


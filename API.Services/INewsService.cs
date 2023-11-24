using API.Domain.Entities;
using API.Domain.ViewModels;

namespace API.Services
{
    public interface INewsService
    {
        Result<NewsViewModel> Get(int page, int qtd);

        NewsViewModel Get(string id);

        NewsViewModel GetBySlug(string slug);

        NewsViewModel Create(NewsViewModel news);

        void Update(string id, NewsViewModel newsIn);

        void Remove(string id);
    }
}


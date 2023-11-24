using API.Domain.Entities;

namespace API.DAL.IRepositories
{
	public interface IMongoRepository<T>
	{
		Result<T> Get(int page, int qtd);

		T Get(string id);

		T GetBySlug(string slug);

        T Create(T news);

		void Update(string id, T news);

		void Remove(string id);

	}
}


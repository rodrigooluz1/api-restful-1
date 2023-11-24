using API.Domain.Entities;
using API.Domain.ViewModels;

namespace API.Services
{
    public interface IGalleryService
    {
        Result<GalleryViewModel> Get(int page, int qtd);

        GalleryViewModel Get(string id);

        GalleryViewModel GetBySlug(string slug);

        GalleryViewModel Create(GalleryViewModel gallery);

        void Update(string id, GalleryViewModel gallery);

        void Remove(string id);
    }
}


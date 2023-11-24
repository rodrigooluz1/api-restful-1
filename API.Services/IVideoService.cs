using API.Domain.Entities;
using API.Domain.ViewModels;

namespace API.Services
{
    public interface IVideoService
    {
        Result<VideoViewModel> Get(int page, int qtd);

        VideoViewModel Get(string id);

        VideoViewModel GetBySlug(string slug);

        VideoViewModel Create(VideoViewModel news);

        void Update(string id, VideoViewModel newsIn);

        void Remove(string id);
    }
}


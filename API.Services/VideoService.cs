using System;
using API.DAL.IRepositories;
using API.Domain.Entities;
using API.Domain.ViewModels;
using AutoMapper;

namespace API.Services
{
	public class VideoService : IVideoService
	{
        private readonly IMapper _mapper;

        private readonly IMongoRepository<Video> _video;

		public VideoService(IMapper mapper, IMongoRepository<Video> video) {
            _mapper = mapper;
            _video = video;
        }

        public Result<VideoViewModel> Get(int page, int qtd) => _mapper.Map<Result<VideoViewModel>>(_video.Get(page, qtd));

        public VideoViewModel Get(string id) => _mapper.Map<VideoViewModel>(_video.Get(id));

        public VideoViewModel GetBySlug(string slug) => _mapper.Map<VideoViewModel>(_video.GetBySlug(slug));

        public VideoViewModel Create(VideoViewModel video)
        {
            var entity = new Video(video.Hat, video.Title, video.Author, video.Thumbnail, video.UrlVideo, video.Status);
            _video.Create(entity);

            return Get(entity.ID);
        }

        public void Update(string id, VideoViewModel video)
        {
            _video.Update(id, _mapper.Map<Video>(video));
        }

        public void Remove(string id) => _video.Remove(id);

        
    }
}


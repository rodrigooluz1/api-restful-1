using System;
using API.DAL.IRepositories;
using API.Domain.Entities;
using API.Domain.ViewModels;
using AutoMapper;
using SharpCompress.Common;

namespace API.Services
{
	public class VideoService : IVideoService
	{
        private readonly IMapper _mapper;

        private readonly IMongoRepository<Video> _video;
        private readonly ICacheService _cacheService;
        private readonly string keyForCache = "video";

        public VideoService(IMapper mapper, IMongoRepository<Video> video, ICacheService cacheService) {
            _mapper = mapper;
            _video = video;
            _cacheService = cacheService;
        }

        //public Result<VideoViewModel> Get(int page, int qtd) => _mapper.Map<Result<VideoViewModel>>(_video.Get(page, qtd));

        public Result<VideoViewModel> Get(int page, int qtd)
        {
            var key = $"{keyForCache}/{page}/{qtd}";
            var  result = _cacheService.Get<Result<VideoViewModel>>(key);

            if(result is null)
            {
                result = _mapper.Map<Result<VideoViewModel>>(_video.Get(page, qtd));
                _cacheService.Set<Result<VideoViewModel>>(key, result);
            }

            return result;

        }

        //public VideoViewModel Get(string id) => _mapper.Map<VideoViewModel>(_video.Get(id));
        public VideoViewModel Get(string id)
        {
            var key = $"{keyForCache}/{id}";

            var result = _cacheService.Get<VideoViewModel>(key);

            if(result is not null)
            {
                result = _mapper.Map<VideoViewModel>(_video.Get(id));
                _cacheService.Set(key, result);
            }

            return result;
        }

        //public VideoViewModel GetBySlug(string slug) => _mapper.Map<VideoViewModel>(_video.GetBySlug(slug));

        public VideoViewModel GetBySlug(string slug)
        {
            var key = $"{keyForCache}/{slug}";

            var result = _cacheService.Get<VideoViewModel>(key);

            if (result is not null)
            {
                result = _mapper.Map<VideoViewModel>(_video.Get(slug));
                _cacheService.Set(key, result);
            }

            return result;
        }

        public VideoViewModel Create(VideoViewModel video)
        {
            var entity = new Video(video.Hat, video.Title, video.Author, video.Thumbnail, video.UrlVideo, video.Status);
            _video.Create(entity);

            var key = $"{keyForCache}/{entity.Slug}";
            _cacheService.Set(key, entity);

            return Get(entity.ID);
        }

        public void Update(string id, VideoViewModel video)
        {
            _video.Update(id, _mapper.Map<Video>(video));

            var key = $"{keyForCache}/{video.Slug}";
            _cacheService.Remove(key);
            _cacheService.Set(key, video);
        }

        //public void Remove(string id) => _video.Remove(id);
        public void Remove(string id) {
            _video.Remove(id);

            var video = Get(id);
            var key = $"{keyForCache}/{video.Slug}";
            _cacheService.Remove(key);
        }

    }
}


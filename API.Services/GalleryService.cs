using System;
using API.DAL.IRepositories;
using API.Domain.Entities;
using API.Domain.ViewModels;
using AutoMapper;
using SharpCompress.Common;

namespace API.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IMapper _mapper;

        private readonly IMongoRepository<Gallery> _gallery;
        private readonly ICacheService _cacheService;
        private readonly string keyForCache = "gallery";

        public GalleryService(IMapper mapper, IMongoRepository<Gallery> gallery, ICacheService cacheService)
        {
            _mapper = mapper;
            _gallery = gallery;
            _cacheService = cacheService;
        }

        //public Result<GalleryViewModel> Get(int page, int qtd) => _mapper.Map<Result<GalleryViewModel>>(_gallery.Get(page, qtd));

        public Result<GalleryViewModel> Get(int page, int qtd)
        {
            var keyCache = $"{keyForCache}/{page}/{qtd}";
            var gallery = _cacheService.Get<Result<GalleryViewModel>>(keyCache);

            if(gallery is null)
            {
                gallery = _mapper.Map<Result<GalleryViewModel>>(_gallery.Get(page, qtd));
                _cacheService.Set(keyCache, gallery);
            }

            return gallery;
        }

        //public GalleryViewModel Get(string id) => _mapper.Map<GalleryViewModel>(_gallery.Get(id));

        public GalleryViewModel Get(string id)
        {
            var keyCache = $"{keyForCache}/{id}";
            var gallery = _cacheService.Get<GalleryViewModel>(keyCache);

            if (gallery is null)
            {
                gallery = _mapper.Map<GalleryViewModel>(_gallery.Get(id));
                _cacheService.Set(keyCache, gallery);
            }

            return gallery;
        }

        //public GalleryViewModel GetBySlug(string slug) => _mapper.Map<GalleryViewModel>(_gallery.GetBySlug(slug));

        public GalleryViewModel GetBySlug(string slug) {
            var keyCache = $"{keyForCache}/{slug}";
            var gallery = _cacheService.Get<GalleryViewModel>(keyCache);

            if (gallery is null)
            {
                gallery = _mapper.Map<GalleryViewModel>(_gallery.Get(slug));
                _cacheService.Set(keyCache, gallery);
            }

            return gallery;
        }

        public GalleryViewModel Create(GalleryViewModel gallery)
        {
            var entity = new Gallery(gallery.Title, gallery.Legend, gallery.Tags, gallery.Author,gallery.Thumbnail, gallery.GalleryImages, gallery.Status);
            _gallery.Create(entity);

            var keyCache = $"{keyForCache}/{entity.Slug}";
            _cacheService.Set(keyCache, entity);

            return Get(entity.ID);
        }

        public void Update(string id, GalleryViewModel gallery)
        {
            _gallery.Update(id, _mapper.Map<Gallery>(gallery));

            
            var keyCache = $"{keyForCache}/{gallery.Id}";
            _cacheService.Remove(keyCache);
            _cacheService.Set(keyCache, gallery);

        }

        //public void Remove(string id) => _gallery.Remove(id);
        public void Remove(string id)
        {
            _gallery.Remove(id);
            var keyCache = $"{keyForCache}/{id}";
            _cacheService.Remove(keyCache);
        }


    }
}



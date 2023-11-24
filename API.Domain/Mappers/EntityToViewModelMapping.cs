using API.Domain.Entities;
using API.Domain.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Domain.Mappers
{
    public class EntityToViewModelMapping : Profile
    {
        public EntityToViewModelMapping()
        {
            CreateMap<News, NewsViewModel>();
            CreateMap<Video, VideoViewModel>().ReverseMap();
            CreateMap<Gallery, GalleryViewModel>().ReverseMap();


            CreateMap<Result<News>, Result<NewsViewModel>>().ReverseMap();
            CreateMap<Result<Video>, Result<VideoViewModel>>().ReverseMap();
            CreateMap<Result<Gallery>, Result<GalleryViewModel>>().ReverseMap();

        }
    }
}

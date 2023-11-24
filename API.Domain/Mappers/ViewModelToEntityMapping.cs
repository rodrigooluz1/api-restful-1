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
    public class ViewModelToEntityMapping : Profile
    {
        public ViewModelToEntityMapping()
        {
            CreateMap<NewsViewModel, News>();
            //CreateMap<VideoViewModel, Video>(); como usei reverseMap no outro arquivo, já não precisa dessa conversaão

        }
    }
}

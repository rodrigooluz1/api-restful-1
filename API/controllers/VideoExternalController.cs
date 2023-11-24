using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.controllers;
using API.Domain.Entities;
using API.Domain.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoExternalController : ControllerBase
    {
        private readonly ILogger<VideoExternalController> _looger;
        private readonly IVideoService _videoService;

        public VideoExternalController(ILogger<VideoExternalController> looger, IVideoService newsService)
        {
            _looger = looger;
            _videoService = newsService;
        }

        [HttpGet]
        public ActionResult<Result<VideoViewModel>> Get(int page, int qtd) => _videoService.Get(page, qtd);

        [HttpGet("{slug}")]
        public ActionResult<VideoViewModel> Get(string slug)
        {

            var video = _videoService.GetBySlug(slug);

            if (video is null)
                return NotFound();

            return video;
        }
    }
}


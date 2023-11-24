using API.Domain.Entities;
using API.Domain.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly ILogger<VideosController> _looger;
        private readonly IVideoService _videoService;

        public VideosController(ILogger<VideosController> looger, IVideoService videoSrvice)
        {
            _looger = looger;
            _videoService = videoSrvice;
        }

        [HttpGet]
        public ActionResult<Result<VideoViewModel>> Get(int page, int qtd) => _videoService.Get(page, qtd);


        [HttpGet("{id:length(24)}", Name = "GetVideos")]
        public ActionResult<VideoViewModel> Get(string id) {

            var video = _videoService.Get(id);

            if (video is null)
                return NotFound();

            return video;
        }


        [HttpPost]
        public ActionResult<VideoViewModel> Create(VideoViewModel video)
        {
            var result = _videoService.Create(video);

            return CreatedAtRoute("GetVideos", new { id = result.Id.ToString() }, result);
        }

        [HttpPut]
        public ActionResult Update(string id, VideoViewModel video)
        {
            var videoUpd = _videoService.Get(id);

            if (videoUpd is null)
                return NotFound();


            _videoService.Update(id, video);

            return CreatedAtRoute("GetVideos", new { id = id }, video);
        }

        [HttpDelete("{ïd:length(24)}")]
        public ActionResult Delete(string id)
        {
            var videoDel = _videoService.Get(id);

            if (videoDel is null)
                return NotFound();

            _videoService.Remove(id);

            return Ok("Vídeo deletado com sucesso");
        }
    }
}

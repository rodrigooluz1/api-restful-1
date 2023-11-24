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
    public class GalleryController : ControllerBase
    {
        private readonly ILogger<GalleryController> _looger;
        private readonly IGalleryService _galleryService;

        public GalleryController(ILogger<GalleryController> looger, IGalleryService galleryService)
        {
            _looger = looger;
            _galleryService = galleryService;
        }

        [HttpGet]
        public ActionResult<Result<GalleryViewModel>> Get(int page, int qtd) => _galleryService.Get(page, qtd);


        [HttpGet("{id:length(24)}", Name = "GetGalerias")]
        public ActionResult<GalleryViewModel> Get(string id)
        {

            var video = _galleryService.Get(id);

            if (video is null)
                return NotFound();

            return video;
        }


        [HttpPost]
        public ActionResult<GalleryViewModel> Create(GalleryViewModel gallery)
        {
            var result = _galleryService.Create(gallery);

            return CreatedAtRoute("GetVideos", new { id = result.Id.ToString() }, result);
        }

        [HttpPut]
        public ActionResult Update(string id, GalleryViewModel gallery)
        {
            var galleryUpd = _galleryService.Get(id);

            if (galleryUpd is null)
                return NotFound();


            _galleryService.Update(id, gallery);

            return CreatedAtRoute("GetGalerias", new { id = id }, gallery);
        }

        [HttpDelete("{ïd:length(24)}")]
        public ActionResult Delete(string id)
        {
            var galleryDel = _galleryService.Get(id);

            if (galleryDel is null)
                return NotFound();

            _galleryService.Remove(id);

            return Ok("Galeria deletada com sucesso");
        }
    }
}

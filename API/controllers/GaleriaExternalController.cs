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
    public class GalleryExternalController : ControllerBase
    {
        private readonly ILogger<GalleryExternalController> _looger;
        private readonly IGalleryService _galleryService;

        public GalleryExternalController(ILogger<GalleryExternalController> looger, IGalleryService galleryService)
        {
            _looger = looger;
            _galleryService = galleryService;
        }

        [HttpGet]
        public ActionResult<Result<GalleryViewModel>> Get(int page, int qtd) => _galleryService.Get(page, qtd);

        [HttpGet("{slug}")]
        public ActionResult<GalleryViewModel> Get(string slug)
        {

            var video = _galleryService.GetBySlug(slug);

            if (video is null)
                return NotFound();

            return video;
        }
    }
}


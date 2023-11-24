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
    public class NewsExternalController : ControllerBase
    {
        private readonly ILogger<NewsController> _looger;
        private readonly INewsService _newsService;

        public NewsExternalController(ILogger<NewsController> looger, INewsService newsService)
        {
            _looger = looger;
            _newsService = newsService;
        }

        [HttpGet]
        public ActionResult<Result<NewsViewModel>> Get(int page, int qtd) => _newsService.Get(page, qtd);

        [HttpGet("{slug}")]
        public ActionResult<NewsViewModel> Get(string slug)
        {

            var news = _newsService.GetBySlug(slug);

            if (news is null)
                return NotFound();

            return news;
        }
    }
}


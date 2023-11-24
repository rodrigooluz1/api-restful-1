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
    public class NewsController : ControllerBase
    {
        private readonly ILogger<NewsController> _looger;
        private readonly INewsService _newsService;

        public NewsController(ILogger<NewsController> looger, INewsService newsService)
        {
            _looger = looger;
            _newsService = newsService;
        }

        [HttpGet]
        public ActionResult<List<NewsViewModel>> Get() => _newsService.Get();


        [HttpGet("{id:length(24)}", Name = "GetNews")]
        [Route("Id")]
        public ActionResult<NewsViewModel> GetById(string id) {

            var news = _newsService.Get(id);

            if (news is null)
                return NotFound();

            return news;
        }


        [HttpPost]
        public ActionResult<NewsViewModel> Create(NewsViewModel news)
        {
            var result = _newsService.Create(news);

            return CreatedAtRoute("GetNews", new { id = result.Id.ToString() }, result);
        }

        [HttpPut]
        public ActionResult Update(string id, NewsViewModel news)
        {
            var newsUpd = _newsService.Get(id);

            if (newsUpd is null)
                return NotFound();


            _newsService.Update(id, news);

            return CreatedAtRoute("GetNews", new { id = id }, news);
        }

        [HttpDelete("{ïd:length(24)}")]
        public ActionResult Delete(string id)
        {
            var newsDel = _newsService.Get(id);

            if (newsDel is null)
                return NotFound();

            _newsService.Remove(id);

            return Ok("Notícia deletada com sucesso");
        }
    }
}

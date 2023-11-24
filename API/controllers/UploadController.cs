using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.controllers;
using API.Util;
using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private readonly ILogger<NewsController> _looger;
        private readonly Upload _upload;

        public UploadController(ILogger<NewsController> looger, Upload upload)
        {
            _looger = looger;
            _upload = upload;
        }

        [HttpPost]
        public IActionResult Post(IFormFile file){
            try
            {
                if (file == null)
                    return null;

                var urlFile = _upload.UploadFile(file);


                return Ok(new
                {
                    mensagem = "Arquivo salvo com sucersso!",
                    urlImagem = urlFile
                });

            }
            catch(Exception ex)
            {
                return StatusCode(500, "Erro no upload: " + ex.Message);
            }
        }
    }
}


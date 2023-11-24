﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly TokenService _tokenService;

        public AuthController(ILogger<AuthController> logger, TokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        [HttpPost]
        public ActionResult Post([FromBody]UserViewModel request)
        {
            string token;
            if(_tokenService.IsAuthenticated(request, out token))
            {
                return Ok(new { token = token });
            }
            else
            {
                return BadRequest("Usuário ou senha inválidos!");
            }
        }
    }
}


﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using thirdparty_api;
using web_api.Models;
using web_api.Repository.Interfaces;

namespace web_api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepo _newsRepo;
        public NewsController(IServiceProvider serviceProvider)
        {
            _newsRepo = serviceProvider.GetRequiredService<INewsRepo>();
        }

        [HttpGet]
        public async Task<IActionResult> Search(string newsTitle)
        {
            NewsDataAPIModel searchResponse = await _newsRepo.searchNews(newsTitle);
            return Ok(searchResponse);
        }
    }
}

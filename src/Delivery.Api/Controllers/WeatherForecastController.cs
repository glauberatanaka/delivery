using Delivery.Core.Entities;
using Delivery.Core.Entities.ProdutoAggregate;
using Delivery.Core.Entities.ProdutoAggregate.Specifications;
using Delivery.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Administrators", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IRepository<Produto> _produtoRepository;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRepository<Produto> produtoController)
        {
            _produtoRepository = produtoController;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var spec = new ProdutoByIdSpec(1);
            var teste3 = await _produtoRepository.GetBySpecAsync(spec);
            var teste = await _produtoRepository.ListAsync();
            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
            return Ok(teste);
        }
    }
}

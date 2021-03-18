using CloudDesignPatterns.Contract;
using CloudDesignPatterns.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CloudDesignPatternController : ControllerBase
    {
        protected IAmbassador _ambassador;
        protected CacheAside<string, IEnumerable<WeatherForecast>> _cache;
        public CloudDesignPatternController(IAmbassador ambassador, CacheAside<string, IEnumerable<WeatherForecast>> cacheAside)
        {
            _ambassador = ambassador;
            _cache = cacheAside;
        }

        [HttpGet("GetWeatherData")]
        public async Task<IActionResult> GetWeatherData()
        {
            return new JsonResult(await _ambassador.Get<IEnumerable<WeatherForecast>>("WeatherForecast"));
        }

        [HttpGet("GetCachedWeatherData")]
        public async Task<IActionResult> GetCachedWeatherData()
        {
            return new JsonResult(await _cache.Execute(typeof(WeatherForecast).Name));
        }
    }
}

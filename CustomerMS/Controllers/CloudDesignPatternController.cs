using CloudDesignPatterns.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CloudDesignPatternController : ControllerBase
    {
        protected IAmbassador _ambassador;
        public CloudDesignPatternController(IAmbassador ambassador)
        {
            _ambassador = ambassador;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherData()
        {
            return new JsonResult(await _ambassador.Get<IEnumerable<WeatherForecast>>("WeatherForecast"));
        }
    }
}

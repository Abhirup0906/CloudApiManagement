using CloudDesignPatterns.Contract;
using CloudDesignPatterns.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDesignPatterns.Service
{
    /*
     * Here this one is implemented as CQRS pattern where this one is the implementation of query.
     * Understanding here is we get the data from db then push it to cache and later phase we use cache data
     * 
     */ 
    public class CustomerMSCacheAside: CacheAside<string, IEnumerable<WeatherForecast>>
    {
        private readonly ICache<WeatherForecast> _cache;
        private readonly IAmbassador _ambassador;
        public CustomerMSCacheAside(ICache<WeatherForecast> cache, IAmbassador ambassador) : base()
        {
            _cache = cache;
            _ambassador = ambassador;
        }

        protected override async Task<IEnumerable<WeatherForecast>> ExecuteInCache(string request)
        {
            return await _cache.Get(typeof(WeatherForecast).Name);
        }

        protected override async Task<IEnumerable<WeatherForecast>> ExecuteInDb(string request)
        {
            var result = await _ambassador.Get<IEnumerable<WeatherForecast>>("WeatherForecast");
            await _cache.Save(result.ToArray(), typeof(WeatherForecast).Name);
            return result;
        }
    }
}

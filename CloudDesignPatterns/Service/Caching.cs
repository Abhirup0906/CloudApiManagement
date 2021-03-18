using CloudDesignPatterns.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDesignPatterns.Service
{
    public class Caching<T> : ICache<T> where T: class
    {
        /*
         * For a demo purpose the implementation contains Dictionary as Cache
         * For demo purpose T is a class, in real it can be a type of any base class or interface
         * In real situation the dictionary will be replaced by redis or other cache implementation 
         * This class has the responsibility to save and get the data from cache. 
         * This implementation will be injected whereever caching is required.
         */ 
        private Dictionary<string, T[]> cache = new Dictionary<string, T[]>();
        public bool IsCacheEnabled { get; set; }

        public async Task<IEnumerable<T>> Get(string key)
        {
            cache.TryGetValue(key, out T[] value);
            return await Task.FromResult(value);
        }

        public async Task<bool> Save(T[] request, string key)
        {
            var result = cache.TryAdd(key, request);
            return await Task.FromResult(result);
        }
    }
}

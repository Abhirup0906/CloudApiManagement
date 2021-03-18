using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDesignPatterns.Contract
{
    public interface ICache<T> where T: class
    {
        public bool IsCacheEnabled { get; set; }
        Task<bool> Save(T[] request, string key);
        Task<IEnumerable<T>> Get(string key);
    }
}

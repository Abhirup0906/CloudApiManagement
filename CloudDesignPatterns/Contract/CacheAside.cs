using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDesignPatterns.Contract
{
    /*
     * As this is not shared with customer so implemented it as abstract class.  
     * This is internal to our own implementation any change we need to rebuild but wont affect distribution 
     * For any new implementation this abstract class needs to be inherited, reference should be registered in DI
     */ 
    public abstract class CacheAside<T, U> 
        where T: class
        where U: class
    {
        public bool IsCacheEnable { get; protected set; } = true;
        public async Task<U> Execute(T request)
        {
            var result = default(U);
            if (IsCacheEnable)
                result = await ExecuteInCache(request);
            if (result is default(U))
                result = await ExecuteInDb(request);
            return result;
        }
        protected abstract Task<U> ExecuteInDb(T request);
        protected abstract Task<U> ExecuteInCache(T request);
    }
}

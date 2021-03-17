using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDesignPatterns.Contract
{
    public interface IAmbassador
    {
        //all http communications (Get, Post, Put, Delete) needs to be implemented here
        Task<T> Get<T>(string requestPath); 
    }
}

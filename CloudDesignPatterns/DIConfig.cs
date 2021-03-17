using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudDesignPatterns.Contract;
using CloudDesignPatterns.Service;
using Polly;
using System.Net.Http;
using Polly.Extensions.Http;

namespace CloudDesignPatterns
{
    public static class DIConfig
    {
        public static void Register(IServiceCollection services)
        {
            services.AddHttpClient<IAmbassador, Ambassador>(client => {
                //Url will come from configuration 
                client.BaseAddress = new Uri("https://localhost:44352/");
            }).AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                            retryAttempt)));
        }

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}

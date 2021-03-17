using CloudDesignPatterns.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CloudDesignPatterns.Service
{
    public class Ambassador : IAmbassador
    {
        /*
         * In microservice world an application need to call multiple microservices. Using Ambassador
         * pattern we can implement resiliency in a cantralized library. 
         * Main purpose of this layer
         * 1. Receive request from client application
         * 2. Determine the location of remote service and route request appropriately
         * 3. check for circuit breaker, retry, timeout etc. policies 
         * 4. Enridh request header with tracing and other informations
         * 5. measuring reqtest latancy 
         * 6. encypt and send request using mutual certificate based authentication 
         * 7. log request latancy
         * 8. return response to client
         */ 
        private HttpClient _httpClient;

        public Ambassador(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Dictionary<string, string> HeaderProperties { get; set; } = new Dictionary<string, string>();
        public string ContentType { get; set; } = "application/json";
        public async Task<T> Get<T>(string requestUri)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(ContentType));
            PopulateHeader();
            var response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseMessage);
            }
            else return default(T);
        }

        private void PopulateHeader()
        {
            foreach (var element in HeaderProperties)
            {
                _httpClient.DefaultRequestHeaders.Add(element.Key, element.Value);
            }
        }
    }
}

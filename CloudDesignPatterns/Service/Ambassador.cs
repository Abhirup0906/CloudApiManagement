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

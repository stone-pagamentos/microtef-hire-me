using AmonRa.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmonRa.Services
{
    public class TransactionList
    {
        private readonly HttpClient _httpClient;

        public TransactionList(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> GetAllTransactionList(string url)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    List<Models.TransactionList> transactionList = JsonConvert.DeserializeObject<List<Models.TransactionList>>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = transactionList
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<Models.TransactionList>()
                };
            }
        }  
    }
}

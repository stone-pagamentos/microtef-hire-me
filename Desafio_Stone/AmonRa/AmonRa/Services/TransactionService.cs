using AmonRa.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AmonRa.Services
{
    public class TransactionService
    {
        private readonly HttpClient _httpClient;

        public TransactionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> PostTransaction(string url, Transaction transaction)
        {
            using (HttpResponseMessage response = await _httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(transaction), Encoding.UTF8, "application/json")))
            {
                dynamic contents = await response.Content.ReadAsStringAsync();
                dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                dynamic data;
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    data = message.errors;
                }
                else
                {
                    Transaction infoTransaction = JsonConvert.DeserializeObject<Transaction>(JsonConvert.SerializeObject(message.data));
                    data = infoTransaction;
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = data
                };
            }
        }

        public async Task<dynamic> PutTransaction(string url, Transaction transaction)
        {
            using (HttpResponseMessage response = await _httpClient.PutAsync(url, new StringContent(JsonConvert.SerializeObject(transaction), Encoding.UTF8, "application/json")))
            {
                dynamic contents = await response.Content.ReadAsStringAsync();
                dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                dynamic data;
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    data = message.errors;
                }
                else
                {
                    Transaction infoTransaction = JsonConvert.DeserializeObject<Transaction>(JsonConvert.SerializeObject(message.data));
                    data = infoTransaction;
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = data
                };
            }
        }

        public async Task<dynamic> GetAllTransactions(string url)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    List<Transaction> listTransaction = JsonConvert.DeserializeObject<List<Transaction>>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = listTransaction
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<Transaction>()
                };
            }
        }

        public async Task<dynamic> GetByNameTransaction(string url, string name)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + name))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    Transaction transaction = JsonConvert.DeserializeObject<Transaction>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = transaction
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<Transaction>()
                };
            }
        }

        public async Task<dynamic> GetByIdTransaction(string url, string id)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + id))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    Transaction transaction = JsonConvert.DeserializeObject<Transaction>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = transaction
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<Transaction>()
                };
            }
        }

        public async Task<dynamic> GetHistorycByIdTransaction(string url, string id)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + "history/" + id))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    List<TransactionHistoric> listHistoricTransaction = JsonConvert.DeserializeObject<List<TransactionHistoric>>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = listHistoricTransaction
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<TransactionHistoric>()
                };
            }
        }

        public async Task<dynamic> DeleteByIdTransaction(string url, Guid id)
        {
            using (HttpResponseMessage response = await _httpClient.DeleteAsync(url + "?id=" + id))
            {
                dynamic contents = await response.Content.ReadAsStringAsync();
                dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                dynamic data;
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    data = message.errors;
                }
                else
                {
                    Transaction infoTransaction = JsonConvert.DeserializeObject<Transaction>(JsonConvert.SerializeObject(message.data));
                    data = infoTransaction;
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = data
                };
            }
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnitTesteKarnakStone.Models;

namespace UnitTesteKarnakStone.Services
{
    public class TransactionStatusService
    {
        private readonly HttpClient _httpClient;

        public TransactionStatusService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> PostTransactionStatus(string url, TransactionStatus cardType)
        {
            using (HttpResponseMessage response = await _httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(cardType), Encoding.UTF8, "application/json")))
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
                    TransactionStatus infoTransactionStatus = JsonConvert.DeserializeObject<TransactionStatus>(JsonConvert.SerializeObject(message.data));
                    data = infoTransactionStatus;
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = data
                };
            }
        }

        public async Task<dynamic> PutTransactionStatus(string url, TransactionStatus cardType)
        {
            using (HttpResponseMessage response = await _httpClient.PutAsync(url, new StringContent(JsonConvert.SerializeObject(cardType), Encoding.UTF8, "application/json")))
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
                    TransactionStatus infoTransactionStatus = JsonConvert.DeserializeObject<TransactionStatus>(JsonConvert.SerializeObject(message.data));
                    data = infoTransactionStatus;
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = data
                };
            }
        }

        public async Task<dynamic> GetAllTransactionStatuss(string url)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    List<TransactionStatus> listTransactionStatus = JsonConvert.DeserializeObject<List<TransactionStatus>>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = listTransactionStatus
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<TransactionStatus>()
                };
            }
        }

        public async Task<dynamic> GetByNameTransactionStatus(string url, string name)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + name))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    TransactionStatus cardType = JsonConvert.DeserializeObject<TransactionStatus>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = cardType
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<TransactionStatus>()
                };
            }
        }

        public async Task<dynamic> GetByIdTransactionStatus(string url, string id)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + id))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    TransactionStatus cardType = JsonConvert.DeserializeObject<TransactionStatus>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = cardType
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<TransactionStatus>()
                };
            }            
        }

        public async Task<dynamic> GetHistorycByIdTransactionStatus(string url, string id)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + "history/" + id))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    List<TransactionStatusHistoric> listHistoricTransactionStatus = JsonConvert.DeserializeObject<List<TransactionStatusHistoric>>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = listHistoricTransactionStatus
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<TransactionStatusHistoric>()
                };
            }
        }

        public async Task<dynamic> DeleteByIdTransactionStatus(string url, Guid id)
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
                    TransactionStatus infoTransactionStatus = JsonConvert.DeserializeObject<TransactionStatus>(JsonConvert.SerializeObject(message.data));
                    data = infoTransactionStatus;
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

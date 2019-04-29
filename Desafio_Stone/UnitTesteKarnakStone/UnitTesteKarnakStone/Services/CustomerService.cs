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
    public class CustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> PostCustomer(string url, Customer cardType)
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
                    Customer infoCustomer = JsonConvert.DeserializeObject<Customer>(JsonConvert.SerializeObject(message.data));
                    data = infoCustomer;
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = data
                };
            }
        }

        public async Task<dynamic> PutCustomer(string url, Customer cardType)
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
                    Customer infoCustomer = JsonConvert.DeserializeObject<Customer>(JsonConvert.SerializeObject(message.data));
                    data = infoCustomer;
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = data
                };
            }
        }

        public async Task<dynamic> GetAllCustomers(string url)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    List<Customer> listCustomer = JsonConvert.DeserializeObject<List<Customer>>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = listCustomer
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<Customer>()
                };
            }
        }

        public async Task<dynamic> GetByNameCustomer(string url, string name)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + name))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    Customer cardType = JsonConvert.DeserializeObject<Customer>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = cardType
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<Customer>()
                };
            }
        }

        public async Task<dynamic> GetByIdCustomer(string url, string id)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + id))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    Customer cardType = JsonConvert.DeserializeObject<Customer>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = cardType
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<Customer>()
                };
            }            
        }

        public async Task<dynamic> GetHistorycByIdCustomer(string url, string id)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + "history/" + id))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    List<CustomerHistoric> listHistoricCustomer = JsonConvert.DeserializeObject<List<CustomerHistoric>>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = listHistoricCustomer
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<CustomerHistoric>()
                };
            }
        }

        public async Task<dynamic> DeleteByIdCustomer(string url, Guid id)
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
                    Customer infoCustomer = JsonConvert.DeserializeObject<Customer>(JsonConvert.SerializeObject(message.data));
                    data = infoCustomer;
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

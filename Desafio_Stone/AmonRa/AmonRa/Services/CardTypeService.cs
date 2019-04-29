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
    public class CardTypeService
    {
        private readonly HttpClient _httpClient;

        public CardTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> PostCardType(string url, CardType cardType)
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
                    CardType infoCardType = JsonConvert.DeserializeObject<CardType>(JsonConvert.SerializeObject(message.data));
                    data = infoCardType;
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = data
                };
            }
        }

        public async Task<dynamic> PutCardType(string url, CardType cardType)
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
                    CardType infoCardType = JsonConvert.DeserializeObject<CardType>(JsonConvert.SerializeObject(message.data));
                    data = infoCardType;
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = data
                };
            }
        }

        public async Task<dynamic> GetAllCardTypes(string url)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    List<CardType> listCardType = JsonConvert.DeserializeObject<List<CardType>>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = listCardType
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<CardType>()
                };
            }
        }

        public async Task<dynamic> GetByNameCardType(string url, string name)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + name))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    CardType cardType = JsonConvert.DeserializeObject<CardType>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = cardType
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<CardType>()
                };
            }
        }

        public async Task<dynamic> GetByIdCardType(string url, string id)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + id))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    CardType cardType = JsonConvert.DeserializeObject<CardType>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = cardType
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<CardType>()
                };
            }
        }

        public async Task<dynamic> GetHistorycByIdCardType(string url, string id)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + "history/" + id))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    List<CardTypeHistoric> listHistoricCardType = JsonConvert.DeserializeObject<List<CardTypeHistoric>>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = listHistoricCardType
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<CardTypeHistoric>()
                };
            }
        }

        public async Task<dynamic> DeleteByIdCardType(string url, Guid id)
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
                    CardType infoCardType = JsonConvert.DeserializeObject<CardType>(JsonConvert.SerializeObject(message.data));
                    data = infoCardType;
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

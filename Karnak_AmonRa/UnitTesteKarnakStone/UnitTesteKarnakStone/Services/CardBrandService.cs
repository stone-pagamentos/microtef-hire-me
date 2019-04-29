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
    public class CardBrandService
    {
        private readonly HttpClient _httpClient;

        public CardBrandService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> PostCardBrand(string url, CardBrand cardType)
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
                    CardBrand infoCardBrand = JsonConvert.DeserializeObject<CardBrand>(JsonConvert.SerializeObject(message.data));
                    data = infoCardBrand;
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = data
                };
            }
        }

        public async Task<dynamic> PutCardBrand(string url, CardBrand cardType)
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
                    CardBrand infoCardBrand = JsonConvert.DeserializeObject<CardBrand>(JsonConvert.SerializeObject(message.data));
                    data = infoCardBrand;
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = data
                };
            }
        }

        public async Task<dynamic> GetAllCardBrands(string url)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    List<CardBrand> listCardBrand = JsonConvert.DeserializeObject<List<CardBrand>>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = listCardBrand
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<CardBrand>()
                };
            }
        }

        public async Task<dynamic> GetByNameCardBrand(string url, string name)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + name))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    CardBrand cardType = JsonConvert.DeserializeObject<CardBrand>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = cardType
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<CardBrand>()
                };
            }
        }

        public async Task<dynamic> GetByIdCardBrand(string url, string id)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + id))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    CardBrand cardType = JsonConvert.DeserializeObject<CardBrand>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = cardType
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<CardBrand>()
                };
            }            
        }

        public async Task<dynamic> GetHistorycByIdCardBrand(string url, string id)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + "history/" + id))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    List<CardBrandHistoric> listHistoricCardBrand = JsonConvert.DeserializeObject<List<CardBrandHistoric>>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = listHistoricCardBrand
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<CardBrandHistoric>()
                };
            }
        }

        public async Task<dynamic> DeleteByIdCardBrand(string url, Guid id)
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
                    CardBrand infoCardBrand = JsonConvert.DeserializeObject<CardBrand>(JsonConvert.SerializeObject(message.data));
                    data = infoCardBrand;
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

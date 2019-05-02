using AmonRa.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AmonRa.Services
{
    public class CardService
    {
        private readonly HttpClient _httpClient;

        public CardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> PostCard(string url, Card card)
        {
            using (HttpResponseMessage response = await _httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json")))
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
                    Card infoCard = JsonConvert.DeserializeObject<Card>(JsonConvert.SerializeObject(message.data));
                    data = infoCard;
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = data
                };
            }
        }

        public async Task<dynamic> PutCard(string url, Card card)
        {
            using (HttpResponseMessage response = await _httpClient.PutAsync(url, new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json")))
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
                    Card infoCard = JsonConvert.DeserializeObject<Card>(JsonConvert.SerializeObject(message.data));
                    data = infoCard;
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = data
                };
            }
        }

        public async Task<dynamic> GetAllCards(string url)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    List<Card> listCard = JsonConvert.DeserializeObject<List<Card>>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = listCard
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<Card>()
                };
            }
        }

        public async Task<dynamic> GetByNameCard(string url, string name)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + name))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    Card card = JsonConvert.DeserializeObject<Card>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = card
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<Card>()
                };
            }
        }

        public async Task<dynamic> GetByIdCard(string url, string id)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + id))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    Card card = JsonConvert.DeserializeObject<Card>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = card
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<Card>()
                };
            }
        }

        public async Task<dynamic> GetByCardNumber(string url, string cardNumber)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + "GetByCardNumber?cardNumber=" + cardNumber))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    Card card = JsonConvert.DeserializeObject<Card>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = card
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<Card>()
                };
            }
        }

        public async Task<dynamic> GetHistorycByIdCard(string url, string id)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync(url + "history/" + id))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    List<CardHistoric> listHistoricCard = JsonConvert.DeserializeObject<List<CardHistoric>>(JsonConvert.SerializeObject(data));
                    return new
                    {
                        Status = response.StatusCode,
                        Data = listHistoricCard
                    };
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = new List<CardHistoric>()
                };
            }
        }

        public async Task<dynamic> DeleteByIdCard(string url, Guid id)
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
                    Card infoCard = JsonConvert.DeserializeObject<Card>(JsonConvert.SerializeObject(message.data));
                    data = infoCard;
                }
                return new
                {
                    Status = response.StatusCode,
                    Data = data
                };
            }
        }

        public async Task<dynamic> GetCardTypeByName(string url, string name)
        {
            string urlInfo = ConfigurationManager.AppSettings["url_servidor_comunicacoes"] + "/cardtype-management/";

            CardType cardType = null;

            using (HttpResponseMessage response = await _httpClient.GetAsync(urlInfo + name))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    cardType = JsonConvert.DeserializeObject<CardType>(JsonConvert.SerializeObject(data));
                }

                return new
                {
                    Status = response.StatusCode,
                    Data = cardType
                };
            }
        }

        public async Task<dynamic> GetCustomerByName(string url, string name)
        {
            string urlInfo = ConfigurationManager.AppSettings["url_servidor_comunicacoes"] + "/customer-management/";

            Customers customer = null;

            using (HttpResponseMessage response = await _httpClient.GetAsync(urlInfo + name))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    customer = JsonConvert.DeserializeObject<Customers>(JsonConvert.SerializeObject(data));
                }

                return new
                {
                    Status = response.StatusCode,
                    Data = customer
                };
            }
        }

        public async Task<dynamic> GetCardBrandByName(string url, string name)
        {
            string urlInfo = ConfigurationManager.AppSettings["url_servidor_comunicacoes"] + "/CardBrand-management/";

            CardBrand cardBrand = null;

            using (HttpResponseMessage response = await _httpClient.GetAsync(urlInfo + name))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic contents = await response.Content.ReadAsStringAsync();
                    dynamic message = JsonConvert.DeserializeObject<ExpandoObject>(contents, new ExpandoObjectConverter());
                    var success = message.success;
                    var data = message.data;
                    cardBrand = JsonConvert.DeserializeObject<CardBrand>(JsonConvert.SerializeObject(data));
                }

                return new
                {
                    Status = response.StatusCode,
                    Data = cardBrand
                };
            }
        }
    }
}

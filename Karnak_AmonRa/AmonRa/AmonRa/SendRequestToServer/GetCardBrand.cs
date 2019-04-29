using AmonRa.Models;
using AmonRa.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmonRa.SendRequestToServer
{
    public class GetCardBrand
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static string requestUri = "https://localhost:44338/api/v1/CardBrand-management/";

        public static async Task<dynamic> Information()
        {
            var serviceBandeiraCartao = new CardBrandService(_httpClient);
            var resultBandeiraCartao = await serviceBandeiraCartao.GetAllCardBrands(requestUri);

            List<CardBrand> cardBrand = new List<CardBrand>();

            foreach (var item in resultBandeiraCartao.Data)
            {
                cardBrand.Add(new CardBrand { Id = item.Id, Name = item.Name });
            }

            return cardBrand;
        }

        public static async Task<dynamic> InformationCombo()
        {
            var serviceBandeiraCartao = new CardBrandService(_httpClient);
            var resultBandeiraCartao = await serviceBandeiraCartao.GetAllCardBrands(requestUri);

            List<string> cardBrand = new List<string>();

            foreach (var item in resultBandeiraCartao.Data)
            {
                cardBrand.Add(item.Name);
            }

            return cardBrand;
        }

        public static async Task<dynamic> GetCardBrandByName(string name)
        {
            var serviceBandeiraCartao = new CardBrandService(_httpClient);
            var resultBandeiraCartao = await serviceBandeiraCartao.GetByNameCardBrand(requestUri, name);

            return resultBandeiraCartao;
        }
    }
}

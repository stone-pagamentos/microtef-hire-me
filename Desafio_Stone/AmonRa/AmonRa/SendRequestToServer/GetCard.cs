using AmonRa.Models;
using AmonRa.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmonRa.SendRequestToServer
{
    public class GetCard
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static string requestUri = "https://localhost:44338/api/v1/Card-management/";

        public static async Task<dynamic> Information()
        {
            var serviceCartao = new CardService(_httpClient);
            var resultCartao = await serviceCartao.GetAllCards(requestUri);

            List<Card> cardBrand = new List<Card>();

            foreach (var item in resultCartao.Data)
            {
                cardBrand.Add(new Card
                {
                    Id = item.Id,
                    IdCardType = item.IdCardType,
                    IdCustomer = item.IdCustomer,
                    IdBrand = item.IdBrand,
                    CardNumber = item.CardNumber,
                    ExpirationDate = item.ExpirationDate,
                    HasPassword = item.HasPassword,
                    Password = item.Password,
                    Limit = item.Limit,
                    LimitAvailable = item.LimitAvailable,
                    Attempts = item.Attempts,
                    Blocked = item.Blocked
                });
            }

            return cardBrand;
        }

        public static async Task<dynamic> PostCard(Card card)
        {
            var serviceCartao = new CardService(_httpClient);
            var resultCartao = await serviceCartao.PostCard(requestUri, card);

            return resultCartao;
        }
    }
}

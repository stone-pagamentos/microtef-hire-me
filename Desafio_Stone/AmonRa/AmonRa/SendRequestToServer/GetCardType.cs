using AmonRa.Models;
using AmonRa.Services;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmonRa.SendRequestToServer
{
    public class GetCardType
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static string requestUri = ConfigurationManager.AppSettings["url_servidor_comunicacoes"] + "/cardtype-management/";

        public static List<CardType> cardType = new List<CardType>();

        public static async Task<dynamic> Information()
        {
            var serviceTipoCartao = new CardTypeService(_httpClient);
            var resultTipoCartao = await serviceTipoCartao.GetAllCardTypes(requestUri);

            foreach (var item in resultTipoCartao.Data)
            {
                cardType.Add(new CardType { Id = item.Id, Name = item.Name });
            }

            return cardType;
        }

        public static async Task<dynamic> GetCardTypeByName(string name)
        {
            var serviceTipoCartao = new CardTypeService(_httpClient);
            var resultTipoCartao = await serviceTipoCartao.GetByNameCardType(requestUri, name);

            return resultTipoCartao;
        }

        public static async Task<dynamic> InformationCombo()
        {
            List<string> tipoCartao = new List<string>();

            var serviceTipoCartao = new CardTypeService(_httpClient);
            var resultTipoCartao = await serviceTipoCartao.GetAllCardTypes(requestUri);

            foreach (var item in resultTipoCartao.Data)
            {
                tipoCartao.Add(item.Name);
            }

            return tipoCartao;
        }
    }
}

using AmonRa.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace AmonRa.SendRequestToServer
{
    public class PostCustomer
    {
        public static async Task<dynamic> SendToServer(
            string nomeCliente,
            string e_mail,
            string aniversario,
            string tipoCartao, 
            string senha,
            string bandeiraCartao,
            string numeroCartao,
            string dataExpiracao,
            string limiteCartao
        )
        {
            dynamic customerStatusRetorno = null;
            dynamic customerDataRetorno = null;

            dynamic cardStatusRetorno = null;
            dynamic cardDataRetorno = null;

            // validar as datas
            DateTime testeData;
            if (!DateTime.TryParse(dataExpiracao, out testeData)
                || !DateTime.TryParse(aniversario, out testeData)
            )
            {
                customerStatusRetorno = HttpStatusCode.BadRequest;

                List<string> dataInvalida = new List<string>();
                dataInvalida.Add("Data Inválida.");

                customerDataRetorno = dataInvalida;

                return new
                {
                    StatusCustomer = customerStatusRetorno,
                    CustomerData = customerDataRetorno,
                    StatusCard = cardStatusRetorno,
                    CardData = cardDataRetorno
                };
            }

            var cardType = await GetCardType.GetCardTypeByName(tipoCartao);
            Guid cardTypeId = new Guid(Convert.ToString(cardType.Data.Id));

            var cardBrand = await GetCardBrand.GetCardBrandByName(bandeiraCartao);
            Guid cardBrandId = new Guid(Convert.ToString(cardBrand.Data.Id));

            // cadastrar cliente
            Customer customerModel = new Customer();
            customerModel.Id = Guid.NewGuid();
            customerModel.Name = nomeCliente;
            customerModel.Email = e_mail;
            customerModel.BirthDate = Convert.ToDateTime(aniversario);

            var customer = await GetCustomer.PostCustomer(customerModel);
            var resultCustomer = customer;

            if (resultCustomer.Status == HttpStatusCode.BadRequest)
            {
                customerDataRetorno = resultCustomer.Data;
                customerStatusRetorno = resultCustomer.Status;
            }
            else
            {
                // cadastrar cartão
                Card card = new Card();
                card.Id = Guid.NewGuid();
                card.IdCardType = cardTypeId;
                card.IdCustomer = customerModel.Id;
                card.IdBrand = cardBrandId;
                card.CardNumber = numeroCartao;
                card.ExpirationDate = Convert.ToDateTime(dataExpiracao);
                card.HasPassword = tipoCartao.ToUpper().Equals("CHIP") ? 1 : 0;
                card.Password = Common.StringCipher.Encrypt(senha, "StefanSilva@#@Stone##2019");
                card.Limit = Convert.ToDecimal(limiteCartao);
                card.LimitAvailable = Convert.ToDecimal(limiteCartao);
                card.Attempts = 0;
                card.Blocked = 1;

                var cardPost = await GetCard.PostCard(card);
                var resultCard = cardPost;

                if (resultCard.Status == HttpStatusCode.BadRequest)
                {
                    // necessario excluir customer
                    var customerDelete = await GetCustomer.DeleteCustomer(customerModel.Id);
                    var resultCustomerDelete = customerDelete;

                    cardDataRetorno = resultCard.Data;
                    cardStatusRetorno = resultCard.Status;
                }
            }

            return new
            {
                StatusCustomer = customerStatusRetorno,
                CustomerData = customerDataRetorno,
                StatusCard = cardStatusRetorno,
                CardData = cardDataRetorno
            };
        }
    }
}

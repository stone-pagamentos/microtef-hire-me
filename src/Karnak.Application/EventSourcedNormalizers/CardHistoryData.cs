using System;

namespace Karnak.Application.EventSourcedNormalizers
{
    public class CardHistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string IdCardType { get; set; }
        public string IdCustomer { get; set; }
        public string IdBrand { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string HasPassword { get; set; }
        public string Password { get; set; }
        public string Limit { get; set; }          
        public string LimitAvailable { get; set; }
        public string Attempts { get; set; }
        public string Blocked { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}
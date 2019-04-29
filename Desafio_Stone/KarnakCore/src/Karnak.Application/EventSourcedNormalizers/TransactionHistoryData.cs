namespace Karnak.Application.EventSourcedNormalizers
{
    public class TransactionHistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string IdTransactionType { get; set; }
        public string IdCard { get; set; }
        public string IdTransactionStatus { get; set; }
        public string Number { get; set; }
        public string TransactionDate { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}
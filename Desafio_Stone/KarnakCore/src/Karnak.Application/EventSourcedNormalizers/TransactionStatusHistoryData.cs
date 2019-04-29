namespace Karnak.Application.EventSourcedNormalizers
{
    public class TransactionStatusHistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}
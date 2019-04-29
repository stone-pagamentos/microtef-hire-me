using System;
using System.Collections.Generic;
using System.Linq;
using Karnak.Domain.Core.Events;
using Newtonsoft.Json;

namespace Karnak.Application.EventSourcedNormalizers
{
    public class TransactionHistory
    {
        public static IList<TransactionHistoryData> HistoryData { get; set; }

        public static IList<TransactionHistoryData> ToJavaScriptTransactionHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<TransactionHistoryData>();
            TransactionHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<TransactionHistoryData>();
            var last = new TransactionHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new TransactionHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? ""
                        : change.Name,
                    Amount = string.IsNullOrWhiteSpace(change.Amount) || change.Amount == last.Amount
                        ? ""
                        : change.Amount,
                    IdTransactionType = string.IsNullOrWhiteSpace(change.IdTransactionType) || change.IdTransactionType == last.IdTransactionType
                        ? ""
                        : change.IdTransactionType,
                    IdCard = string.IsNullOrWhiteSpace(change.IdCard) || change.IdCard == last.IdCard
                        ? ""
                        : change.IdCard,
                    IdTransactionStatus = string.IsNullOrWhiteSpace(change.IdTransactionStatus) || change.IdTransactionStatus == last.IdTransactionStatus
                        ? ""
                        : change.IdTransactionStatus,
                    Number = string.IsNullOrWhiteSpace(change.Number) || change.Number == last.Number
                        ? ""
                        : change.Number,
                    TransactionDate = string.IsNullOrWhiteSpace(change.TransactionDate) || change.TransactionDate == last.TransactionDate
                        ? ""
                        : change.TransactionDate,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void TransactionHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new TransactionHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "TransactionRegisteredEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Name = values["Name"];
                        slot.Amount = values["Amount"];
                        slot.IdTransactionType = values["IdTransactionType"];
                        slot.IdCard = values["IdCard"];
                        slot.IdTransactionStatus = values["IdTransactionStatus"];
                        slot.Number = values["Number"];
                        slot.TransactionDate = values["TransactionDate"];
                        slot.Action = "Registered";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "TransactionUpdatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Name = values["Name"];
                        slot.Amount = values["Amount"];
                        slot.IdTransactionType = values["IdTransactionType"];
                        slot.IdCard = values["IdCard"];
                        slot.IdTransactionStatus = values["IdTransactionStatus"];
                        slot.Number = values["Number"];
                        slot.TransactionDate = values["TransactionDate"];
                        slot.Action = "Updated";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "TransactionRemovedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Action = "Removed";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                }
                HistoryData.Add(slot);
            }
        }
    }
}
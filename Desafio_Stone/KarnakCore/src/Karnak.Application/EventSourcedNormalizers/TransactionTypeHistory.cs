using System;
using System.Collections.Generic;
using System.Linq;
using Karnak.Domain.Core.Events;
using Newtonsoft.Json;

namespace Karnak.Application.EventSourcedNormalizers
{
    public class TransactionTypeHistory
    {
        public static IList<TransactionTypeHistoryData> HistoryData { get; set; }

        public static IList<TransactionTypeHistoryData> ToJavaScriptTransactionTypeHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<TransactionTypeHistoryData>();
            TransactionTypeHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<TransactionTypeHistoryData>();
            var last = new TransactionTypeHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new TransactionTypeHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? ""
                        : change.Name,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void TransactionTypeHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new TransactionTypeHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "TransactionTypeRegisteredEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Name = values["Name"];
                        slot.Action = "Registered";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "TransactionTypeUpdatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Name = values["Name"];
                        slot.Action = "Updated";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "TransactionTypeRemovedEvent":
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
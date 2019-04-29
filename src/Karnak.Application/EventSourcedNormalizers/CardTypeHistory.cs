using System;
using System.Collections.Generic;
using System.Linq;
using Karnak.Domain.Core.Events;
using Newtonsoft.Json;

namespace Karnak.Application.EventSourcedNormalizers
{
    public class CardTypeHistory
    {
        public static IList<CardTypeHistoryData> HistoryData { get; set; }

        public static IList<CardTypeHistoryData> ToJavaScriptCardTypeHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<CardTypeHistoryData>();
            CardTypeHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<CardTypeHistoryData>();
            var last = new CardTypeHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new CardTypeHistoryData
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

        private static void CardTypeHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new CardTypeHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "CardTypeRegisteredEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Name = values["Name"];
                        slot.Action = "Registered";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "CardTypeUpdatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Name = values["Name"];
                        slot.Action = "Updated";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "CardTypeRemovedEvent":
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
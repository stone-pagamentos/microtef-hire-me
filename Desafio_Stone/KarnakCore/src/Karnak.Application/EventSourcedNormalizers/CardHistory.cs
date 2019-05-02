using System;
using System.Collections.Generic;
using System.Linq;
using Karnak.Domain.Core.Events;
using Newtonsoft.Json;

namespace Karnak.Application.EventSourcedNormalizers
{
    public class CardHistory
    {
        public static IList<CardHistoryData> HistoryData { get; set; }

        public static IList<CardHistoryData> ToJavaScriptCardHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<CardHistoryData>();
            CardHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<CardHistoryData>();
            var last = new CardHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new CardHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    IdCardType = string.IsNullOrWhiteSpace(change.IdCardType) || change.IdCardType == last.IdCardType
                        ? ""
                        : change.IdCardType,
                    IdCustomer = string.IsNullOrWhiteSpace(change.IdCustomer) || change.IdCardType == last.IdCustomer
                        ? ""
                        : change.IdCustomer,
                    IdBrand = string.IsNullOrWhiteSpace(change.IdBrand) || change.IdBrand == last.IdBrand
                        ? ""
                        : change.IdBrand,
                    CardNumber = string.IsNullOrWhiteSpace(change.CardNumber) || change.CardNumber == last.CardNumber
                        ? ""
                        : change.CardNumber,
                    ExpirationDate = string.IsNullOrWhiteSpace(change.ExpirationDate) || change.ExpirationDate == last.ExpirationDate
                        ? ""
                        : change.ExpirationDate,
                    HasPassword = string.IsNullOrWhiteSpace(change.HasPassword) || change.HasPassword == last.HasPassword
                        ? ""
                        : change.HasPassword,
                    Password = string.IsNullOrWhiteSpace(change.Password) || change.Password == last.Password
                        ? ""
                        : change.Password,
                    Limit = string.IsNullOrWhiteSpace(change.Limit) || change.Limit == last.Limit
                        ? ""
                        : change.Limit,
                    LimitAvailable = string.IsNullOrWhiteSpace(change.LimitAvailable) || change.LimitAvailable == last.LimitAvailable
                        ? ""
                        : change.LimitAvailable,
                    Attempts = string.IsNullOrWhiteSpace(change.Attempts) || change.Attempts == last.Attempts
                        ? ""
                        : change.Attempts,
                    Blocked = string.IsNullOrWhiteSpace(change.Blocked) || change.Blocked == last.Blocked
                        ? ""
                        : change.Blocked,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void CardHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new CardHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "CardRegisteredEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.IdCardType = values["IdCardType"];
                        slot.IdCustomer = values["IdCustomer"];
                        slot.IdBrand = values["IdBrand"];
                        slot.CardNumber = values["CardNumber"];
                        slot.ExpirationDate = values["ExpirationDate"];
                        slot.HasPassword = values["HasPassword"];
                        slot.Password = values["Password"];
                        slot.Limit = values["Limit"];
                        slot.LimitAvailable = values["LimitAvailable"];
                        slot.Attempts = values["Attempts"];
                        slot.Blocked = values["Blocked"];
                        slot.Action = "Registered";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "CardUpdatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.IdCardType = values["IdCardType"];
                        slot.IdCustomer = values["IdCustomer"];
                        slot.IdBrand = values["IdBrand"];
                        slot.CardNumber = values["CardNumber"];
                        slot.ExpirationDate = values["ExpirationDate"];
                        slot.HasPassword = values["HasPassword"];
                        slot.Password = values["Password"];
                        slot.Limit = values["Limit"];
                        slot.LimitAvailable = values["LimitAvailable"];
                        slot.Attempts = values["Attempts"];
                        slot.Blocked = values["Blocked"];
                        slot.Action = "Updated";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "CardRemovedEvent":
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
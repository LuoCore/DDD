using Domain.CommandEventsHandler.EventHandlers;
using Domain.Models.HistoryModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Domain.EventSourcedNormalizers
{
    /// <summary>
    /// 事件溯源规范化
    /// </summary>
    public class UserHistory
    {
        public static IList<Domain.Models.HistoryModels.UserHistoryModel> HistoryData { get; set; }

        // 将数据从事件源中获取到list中
        public static IList<Domain.Models.HistoryModels.UserHistoryModel> ToJavaScriptStudentHistory(IList<Infrastructure.Entitys.StoredEvent> storedEvents)
        {
            HistoryData = new List<Domain.Models.HistoryModels.UserHistoryModel>();
            HistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<UserHistoryModel>();
            var last = new UserHistoryModel();

            foreach (var change in sorted)
            {
                var jsSlot = new UserHistoryModel
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id? "": change.Id,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name? "": change.Name,
                    Email = string.IsNullOrWhiteSpace(change.Email) || change.Email == last.Email? "": change.Email,
                    Phone = string.IsNullOrWhiteSpace(change.Phone) || change.Phone == last.Phone? "": change.Phone,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        /// <summary>
        /// 将事件源进行反序列化
        /// </summary>
        /// <param name="storedEvents"></param>
        private static void HistoryDeserializer(IEnumerable<Infrastructure.Entitys.StoredEvent> userEvents)
        {
            foreach (var e in userEvents)
            {
                var slot = new UserHistoryModel();
                dynamic values;

                var classes = Assembly.Load("Domain.Models").GetTypes();
                var NamespaceList = classes.Where(x => x.Namespace == "Domain.Models.EventModels.User").ToList();
                foreach (var item in NamespaceList)
                {
                    Console.WriteLine(item.Name);
                    if (e.MessageType == item.Name) 
                    {
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Email = values["Email"];
                        slot.Phone = values["Phone"];
                        slot.Name = values["Name"];
                        slot.Action = "Registered";
                        slot.When = values["Timestamp"];
                        slot.Id = values["UserId"];
                        slot.Who = e.CreateName;
                        HistoryData.Add(slot);
                    }
                }
            }
        }
    }
}

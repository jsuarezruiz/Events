using System;
using System.Diagnostics;
using Microsoft.Phone.Scheduler;
using Recetario.Model;

namespace Recetario.Services.Reminder
{
    public class ReminderService : IReminderService
    {
        public bool IsScheduled(string name)
        {
            ScheduledAction schedule = ScheduledActionService.Find(name);
            return schedule != null && schedule.IsScheduled;
        }

        public void SetReminder(RecipeDataItem item)
        {
            if (!IsScheduled(item.UniqueId))
            {
                ScheduledAction schedule = ScheduledActionService.Find(item.UniqueId);
                if (null != schedule)
                    ScheduledActionService.Remove(schedule.Name);

                var reminder = new Microsoft.Phone.Scheduler.Reminder(item.UniqueId)
                {
                    Title = item.Title,
                    Content = "Has terminado de cocinar?",
                    BeginTime =
                        Debugger.IsAttached
                            ? DateTime.Now.AddSeconds(30)
                            : DateTime.Now.Add(TimeSpan.FromMinutes(Convert.ToDouble(item.PrepTime)))
                };

                reminder.ExpirationTime = reminder.BeginTime.AddSeconds(30);
                reminder.RecurrenceType = RecurrenceInterval.None;
                reminder.NavigationUri =
                    new Uri("../View/RecipeDetailPage.xaml?ID=" + item.UniqueId + "&GID=" + item.Group.UniqueId,
                        UriKind.Relative);

                ScheduledActionService.Add(reminder);
            }
            else
            {
                ScheduledAction schedule = ScheduledActionService.Find(item.UniqueId);
                ScheduledActionService.Remove(schedule.Name);
            }
        }
    }
}
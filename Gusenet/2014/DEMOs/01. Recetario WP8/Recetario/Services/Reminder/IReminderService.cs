using Recetario.Model;

namespace Recetario.Services.Reminder
{
    public interface IReminderService
    {
        bool IsScheduled(string name);
        void SetReminder(RecipeDataItem item);
    }
}

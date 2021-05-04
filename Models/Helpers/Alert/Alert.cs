using MyForum.Core.Enums;

namespace MyForum.Models.Helpers.Alert
{
    public class Alert
    {
        public AlertType Type { get; }
        public string Message { get; }
        public Alert(AlertType type, string message)
        {
            Type = type;
            Message = message;
        }

    }
}
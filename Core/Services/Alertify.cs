using MyForum.Core.Enums;
using MyForum.Models.Helpers.Alert;

namespace MyForum.Core.Services
{
    public sealed class Alertify
    {
        public static Alert Alert { get; private set; }

        public static void Push(string message, AlertType type = AlertType.Info)
        {
            Alert = new Alert(type, message);
        }

        public static void Clear()
        {
            Alert = null;
        }

        public static Alertify Build() => new Alertify();
    }
}
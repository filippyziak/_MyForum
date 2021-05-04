using MyForum.Core.Enums;
using MyForum.Core.Services;
using MyForum.Models.Helpers.Alert;

namespace MyForum.ViewModels
{
    public class BaseViewModel
    {
        public string Title { get; set; }
        public Alert Alert { get; protected set; }

        public BaseViewModel WithAlert(string message, AlertType type = AlertType.Info)
        {
            Alert = new Alert(type, message);
            Alertify.Clear();

            return this;
        }

        public BaseViewModel WithAlert()
        {
            Alert = Alertify.Alert;
            Alertify.Clear();
            return this;
        }
    }
}
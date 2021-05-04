using Microsoft.AspNetCore.Mvc;
using MyForum.Core.Enums;
using MyForum.Core.Services;

namespace MyForum.Core.Extensions
{
    public static class ControllerExtensions
    {
        public static RedirectToActionResult ErrorPage(this Controller controller)
        => controller.RedirectToAction("Error", "Home");

        public static IActionResult PushAlert(this IActionResult view, string message, AlertType type = AlertType.Info)
        {
            Alertify.Push(message, type);
            return view;
        }
    }
}
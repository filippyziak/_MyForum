using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyForum.ViewModels.Components;

namespace MyForum.Components
{
    public class ErrorViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic key = null, string error = null,
            KeyValuePair<dynamic, string> value = default(KeyValuePair<dynamic, string>))
        {
            ViewBag.Key = key;
            ViewBag.Error = error;

            return View(ErrorComponentViewModel.Build(error: value));
        }
    }
}
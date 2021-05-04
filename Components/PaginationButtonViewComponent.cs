using Microsoft.AspNetCore.Mvc;

namespace MyForum.Components
{
    public class PaginationButtonViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string actionName, int pageNumber)
        {
            ViewData["ActionName"] = actionName;
            ViewData["PageNumber"] = pageNumber;

            return View();
        }
    }
}
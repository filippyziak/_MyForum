using Microsoft.AspNetCore.Mvc;
using MyForum.ViewModels;

namespace MyForum.Components
{
    public class ChangeEmailViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ChangeEmailViewModel viewModel) => View(viewModel);
    }
}
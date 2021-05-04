using Microsoft.AspNetCore.Mvc;
using MyForum.ViewModels;

namespace MyForum.Components
{
    public class ChangePasswordViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ChangePasswordViewModel viewModel) => View(viewModel);
    }
}
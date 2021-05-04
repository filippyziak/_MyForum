using Microsoft.AspNetCore.Mvc;
using MyForum.ViewModels;

namespace MyForum.Components
{
    public class CreateAnswerViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(CreateAnswerViewModel viewModel) => View(viewModel);
    }
}
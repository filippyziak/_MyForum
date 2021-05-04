using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyForum.ViewModels;

namespace MyForum.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        => View(new HomeViewModel());

        public IActionResult Error()
        => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
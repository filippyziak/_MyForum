using MyForum.Core.Services;

namespace MyForum.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Title = "Main";
        }

        public void Dispose()
        {
            Alertify.Clear();
        }
    }
}
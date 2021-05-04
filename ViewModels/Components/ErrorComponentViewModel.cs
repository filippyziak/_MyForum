using System.Collections.Generic;

namespace MyForum.ViewModels.Components
{
    public class ErrorComponentViewModel
    {
        public KeyValuePair<dynamic, string> Error { get; set; }

        public static ErrorComponentViewModel Build(KeyValuePair<dynamic, string> error = default(KeyValuePair<dynamic, string>))
            => new ErrorComponentViewModel { Error = error };
    }
}
using System.Collections.Generic;
using MyForum.Core.Services;

namespace MyForum.ViewModels
{
    public abstract class ErrorBaseViewModel : BaseViewModel
    {
        public string Error { get; protected set; }

        public virtual ErrorBaseViewModel WithError()
        {
            Error = ErrorCreator.Error;
            Alert = Alertify.Alert;

            ErrorCreator.Clear();
            Alertify.Clear();

            return this;
        }
    }

    public abstract class ErrorBaseViewModel<T> : BaseViewModel
    {
        public KeyValuePair<dynamic, string> Error { get; protected set; }

        public virtual ErrorBaseViewModel<T> WithError(T key)
        {
            Error = ErrorCreator<T>.Error;
            Alert = Alertify.Alert;

            ErrorCreator<T>.Clear(key);
            Alertify.Clear();

            return this;
        }
    }
}
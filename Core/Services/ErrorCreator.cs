using System.Collections.Generic;

namespace MyForum.Core.Services
{
    public static class ErrorCreator
    {
        public static string Error { get; private set; }

        public static void Append(string error)
        {
            Error = error;
        }

        public static void Clear()
        {
            Error = null;
        }
    }

    public static class ErrorCreator<T>
    {
        public static KeyValuePair<dynamic, string> Error { get; private set; }

        public static void Append(T key, string error)
        {
            Error = new KeyValuePair<dynamic, string>(key, error);
        }

        public static void Clear(T key)
        {
            if (key.Equals(Error.Key))
                Error = default(KeyValuePair<dynamic, string>);
        }
    }
}
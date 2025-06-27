using System;

namespace _403unlocker
{
    internal static class Extention
    {
        public static string GetMessages(this Exception exception)
        {
            string s = exception.Message;
            if (!(exception.InnerException is null))
            {
                s += "\n\n" + GetMessages(exception.InnerException);
            }
            return s;
        }
    }
}

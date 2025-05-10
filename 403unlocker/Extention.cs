using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

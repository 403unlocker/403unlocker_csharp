using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _403unlocker.Library.NotificationMessage
{
    public class NotificationState
    {
        public string State { get; set; }
        public string Message { get; set; }

        public NotificationState(string state, string message)
        {
            State = state;
            Message = message;
        }

        public override string ToString()
        {
            return State.ToString();
        }
    }
}

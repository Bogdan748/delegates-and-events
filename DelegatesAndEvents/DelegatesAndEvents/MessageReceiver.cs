using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEvents
{
    public class MessageReceiver
    {
        public void Receive(string message)
        {
            Console.WriteLine($"MeesageReceiver.Receive :: {message}");
        }
    }
}

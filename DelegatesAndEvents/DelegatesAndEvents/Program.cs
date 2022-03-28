using System;

namespace DelegatesAndEvents
{

    public delegate int Sum3numbers(int n1, int n2, int n3);

    public delegate T Sum3Values<T>(T n1, T n2, T n3);

    public delegate void MessageBrodcastSender(string message);

    class Program
    {



        static void Main(string[] args)
        {
            Calculator c = new Calculator();

            Sum3Values<int> genericFunc = c.Sum;
            int? resultGeneric = genericFunc?.Invoke(1, 2, 3);
            Console.WriteLine(resultGeneric);
            Sum3numbers func = delegate(int a, int b, int c)
            {
                return a + b + c;
            };


            //
            //Sum3numbers func = c.Sum;

            int result = func(1, 2, 3);
            Console.WriteLine(result);



            MessagePublisher publisher = new MessagePublisher();
            MessageReceiver receiver = new MessageReceiver();
            publisher.OnMessageReceived += Program.ReceiveMessage;
            publisher.OnMessageReceived += receiver.Receive;

            publisher.Publish("Hello Events!");

            publisher.OnMessageReceived -= receiver.Receive;

            publisher.Publish("Hello again Events!");
        }
        /*
        private static int Sum(int a, int b, int c)
        {
            return a + b + c;

        }*/

        private static void MessageInvoker (MessageBrodcast sender, string message)
        {
            if (sender is not null)
            {
                sender(message);
            }
            
        }

        private static void ReceiveMessage(string message)
        {
            Console.WriteLine($"Program,ReceiveMessage :: {message}");
        }

        private static void Exemple_With_Delegate()
        {
            MessageReceiver receiver = new MessageReceiver();
            MessageBrodcast sender = null;

            sender += Program.ReceiveMessage;
            sender += receiver.Receive;


            //sender("Hello delegates!");
            MessageInvoker(sender, "Hello delegates!");

            sender -= receiver.Receive;
            sender -= Program.ReceiveMessage;

            MessageInvoker(sender, "Hello delegates!");

        }

    }
}

using System;
using System.Collections.Generic;

namespace DelegatesAndEvents
{

    public delegate int Sum3numbers(int n1, int n2, int n3);

    public delegate T Sum3Values<T>(T n1, T n2, T n3);

    public delegate void MessageBrodcastSender(string message);

   

    class Program
    {

        private static Func<int> Increment()
        {
            int start = 0;
            return () => 
            { 
                start++;
                Console.WriteLine($"Strt is {start}");
                return start; 
            };
        }

        private static Func<int> IncrementOther()
        {
            return () =>
            {
                int start = 0;
                start++;
                Console.WriteLine($"Strt is {start}");
                return start;
            };
        }

        static void Main(string[] args)
        {
            List<Action> listOfActions = new List<Action>();

            for (int i = 0; i < 5; i++)
            {
                int temp = i;
                listOfActions.Add(() => Console.WriteLine($"i={temp}"));
            }

            foreach(Action act in listOfActions)
            {
                act();
            }

            Console.WriteLine("---------------------------------------");

            Func<int> increment = Increment();

            increment();
            increment();
            increment();
            increment();


            int a = 10;

            Action action = () =>
            {
                a = 30;
                Console.WriteLine($"Variable capture: {a}");
            };

            a = 20;

            action();


            Console.WriteLine(a);

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

        private static  void Example_Events()
        {

            MessagePublisher publisher = new MessagePublisher();
            MessageReceiver receiver = new MessageReceiver();
            publisher.OnMessageReceived += Program.ReceiveMessage;
            publisher.OnMessageReceived += receiver.Receive;

            publisher.Publish("Hello Events!");

            publisher.OnMessageReceived -= receiver.Receive;

            publisher.Publish("Hello again Events!");
        }

        private static void Example_Generic_Delegates()
        {
            Calculator c = new Calculator();

            Sum3Values<int> genericFunc = c.Sum;
            int? resultGeneric = genericFunc?.Invoke(1, 2, 3);
            Console.WriteLine(resultGeneric);

            Sum3numbers func = (a, b, c) =>
            {
                return a + b + c;
            };


            //
            //Sum3numbers func = c.Sum;

            int result = func(1, 2, 3);
            Console.WriteLine(result);
        }
    }
}

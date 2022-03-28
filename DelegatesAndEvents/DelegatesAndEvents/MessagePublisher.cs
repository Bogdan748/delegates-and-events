
namespace DelegatesAndEvents
{
    public class MessagePublisher
    {
        private event MessageBrodcast onMessageReceived;

        public event MessageBrodcast OnMessageReceived {
            add { onMessageReceived += value; }
            remove { onMessageReceived -= value; }
        }

        public void Publish(string message)
        {
            onMessageReceived?.Invoke(message);
        }
    }
}

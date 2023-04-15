namespace External.Messages;

public class MessageBroker : IMessagePublisher, IMessageReceiver
{
    private readonly List<object> _receivers = new();

    public void Publish<T>(T message)
    {
        foreach (object receiver in _receivers)
        {
            if (receiver.GetType() == typeof(Action<T>))
                ((Action<T>)receiver).Invoke(message);
        }
    }

    public void Subscribe<T>(Action<T> receiver) =>
        _receivers.Add(receiver);
}
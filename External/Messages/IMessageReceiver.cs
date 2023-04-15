namespace External.Messages;

public interface IMessageReceiver
{
    void Subscribe<T>(Action<T> receiver);
}

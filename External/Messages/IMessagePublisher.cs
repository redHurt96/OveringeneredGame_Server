namespace External.Messages;

public interface IMessagePublisher
{
    void Publish<T>(T message);
}

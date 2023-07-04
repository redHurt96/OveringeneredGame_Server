namespace Server
{
    internal class MoveService
    {
        private readonly ClientsRepository _repository;

        internal MoveService(ClientsRepository repository) => 
            _repository = repository;

        internal UpdatePositionMessage Execute(Guid id, MoveMessage moveCharacterMessage)
        {
            ClientData client = _repository.Get(id);
            client.Position += moveCharacterMessage.Direction * client.Speed * .033f;

            return new UpdatePositionMessage()
            {
                Tick = moveCharacterMessage.Tick,
                Position = client.Position,
            };
        }
    }
}

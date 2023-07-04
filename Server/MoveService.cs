using System.Data;
using System.Numerics;

namespace Server
{
    internal class MoveService
    {
        private readonly ClientsRepository _repository;

        internal MoveService(ClientsRepository repository) => 
            _repository = repository;

        internal UpdateCharacterPositionMessage Execute(MoveCharacterMessage moveCharacterMessage)
        {
            ClientData client = _repository.Get(moveCharacterMessage.Id);
            DateTime now = DateTime.Now;
            Vector3 delta = new Vector3(moveCharacterMessage.X, moveCharacterMessage.Y, moveCharacterMessage.Z);
            client.Position += delta * client.Speed * .033f;

            return new UpdateCharacterPositionMessage()
            {
                Id = moveCharacterMessage.Id,
                X = client.Position.X,
                Y = client.Position.Y,
                Z = client.Position.Z,
            };
        }
    }
}

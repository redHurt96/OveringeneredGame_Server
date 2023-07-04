using Newtonsoft.Json;
using System;

namespace _Project
{
    public class MessagesParser
    {
        public (Type, object) Deserialize(string data)
        {
            string[] splitData = data.Split(';');
            Type target = Type.GetType(splitData[0]);
            object fromJson = JsonConvert.DeserializeObject(splitData[1], target);

            return (target, fromJson);
        }

        public string Serialize<T>(T message) =>
            $"{typeof(T)};{JsonConvert.SerializeObject(message)}";
    }
}
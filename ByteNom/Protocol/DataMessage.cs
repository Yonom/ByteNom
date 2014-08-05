using ProtoBuf;

namespace ByteNom.Protocol
{
    [ProtoContract]
    internal class DataMessage
    {
        public DataMessage()
        {
        }

        public DataMessage(string type, DataItem[] arguments)
        {
            this.Type = type;
            this.Arguments = arguments;
        }

        [ProtoMember(1)]
        public string Type { get; set; }

        [ProtoMember(2)]
        public DataItem[] Arguments { get; set; }
    }
}
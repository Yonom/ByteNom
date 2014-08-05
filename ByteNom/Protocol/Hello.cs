using ProtoBuf;

namespace ByteNom.Protocol
{
    [ProtoContract]
    internal class Hello
    {
        public const int VersionNumber = 1;

        public Hello()
        {
            this.Version = VersionNumber;
        }

        [ProtoMember(1)]
        public int Version { get; set; }
    }
}
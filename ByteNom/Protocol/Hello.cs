using ProtoBuf;

namespace ByteNom
{
    [ProtoContract]
    internal class Hello
    {
        public const int VersionNumber = 1;

        [ProtoMember(1)]
        public int Version { get; set; }

        public Hello()
        {
            this.Version = VersionNumber;
        }
    }
}
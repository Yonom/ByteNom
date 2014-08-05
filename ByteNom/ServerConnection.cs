using System.IO;
using System.Net.Sockets;
using ByteNom.Protocol;

namespace ByteNom
{
    internal class ServerConnection : Connection
    {
        public ServerConnection(TcpClient client)
        {
            this.SetClient(client);
        }

        public new void Start()
        {
            base.Start();
        }

        protected override void Work()
        {
            var hello = this.ProtoGet<Hello>();
            if (hello.Version != Hello.VersionNumber)
            {
                throw new InvalidDataException("Server and Client version numbers do not match.");
            }

            base.Work();
        }
    }
}
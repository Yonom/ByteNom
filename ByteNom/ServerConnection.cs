using System.IO;
using System.Net.Sockets;
using ByteNom.Protocol;

namespace ByteNom
{
    internal class ServerConnection : Connection
    {
        private readonly TcpClient _client;

        public ServerConnection(TcpClient client)
        {
            this._client = client;
        }

        public void Start()
        {
            this.Start(this._client);
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
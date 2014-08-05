using System.Net;
using System.Net.Sockets;
using ByteNom.Protocol;

namespace ByteNom
{
    public class Client : Connection
    {
        private readonly string _hostname;
        private readonly int _port;

        public Client(string hostname, int port)
        {
            this._hostname = hostname;
            this._port = port;
        }

        public void Start()
        {
            this.Start(new TcpClient(_hostname, _port));
        }

        protected override void Work()
        {
            this.ProtoSend(new Hello());
            base.Work();
        }
    }
}
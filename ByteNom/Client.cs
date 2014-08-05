using System.Net.Sockets;
using ByteNom.Protocol;

namespace ByteNom
{
    /// <summary>
    ///     Provides a client connection to a ByteNom server.
    /// </summary>
    public class Client : Connection
    {
        private readonly string _hostname;
        private readonly int _port;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Client" /> class.
        /// </summary>
        /// <param name="hostname">The hostname used when connecting.</param>
        /// <param name="port">The port used when connecting.</param>
        public Client(string hostname, int port)
        {
            this._hostname = hostname;
            this._port = port;
        }

        /// <summary>
        ///     Starts this instance and connects to the server.
        /// </summary>
        public void Connect()
        {
            this.SetClient(new TcpClient(this._hostname, this._port));
            this.ProtoSend(new Hello());
            this.Start();
        }
    }
}
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ByteNom
{
    /// <summary>
    /// Represents a TcpConnection.
    /// </summary>
    public abstract class TcpConnection : Connection
    {
        /// <summary>
        ///     Gets a value indicating whether this <see cref="Connection" /> is connected.
        /// </summary>
        /// <value>
        ///     <c>true</c> if connected; otherwise, <c>false</c>.
        /// </value>
        public override bool Connected
        {
            get
            {
                if (this.Client == null)
                    return false;

                return this.Client.Connected;
            }
        }

        /// <summary>
        ///     Gets the remote end point.
        /// </summary>
        /// <value>
        ///     The remote end point.
        /// </value>
        public override EndPoint EndPoint
        {
            get
            {
                if (this.Client == null)
                    return null;

                return this.Client.Client.RemoteEndPoint;
            }
        }

        /// <summary>
        ///     Gets the stream.
        /// </summary>
        /// <value>
        ///     The stream.
        /// </value>
        protected override Stream Stream { get; set; }

        /// <summary>
        ///     Gets the tcp client.
        /// </summary>
        /// <value>
        ///     The tcp client.
        /// </value>
        protected TcpClient Client { get; private set; }

        /// <summary>
        ///     Sets the internal tcp client for this connection.
        /// </summary>
        /// <param name="client">The client.</param>
        protected void SetClient(TcpClient client)
        {
            this.Client = client;
            this.Stream = client.GetStream();
        }

        /// <summary>
        /// Disconnects from the server.
        /// </summary>
        public override void Disconnect()
        {
            base.Disconnect();
            this.Client.Close();
        }
    }
}

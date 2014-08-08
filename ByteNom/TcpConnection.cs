using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
        public bool Connected
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
        public IPEndPoint EndPoint
        {
            get
            {
                if (this.Client == null)
                    return null;

                return (IPEndPoint)this.Client.Client.RemoteEndPoint;
            }
        }

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
            this.SetStream(client.GetStream());
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

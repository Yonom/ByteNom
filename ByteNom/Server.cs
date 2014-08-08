using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ByteNom
{
    /// <summary>
    ///     Listens for ByteNom clients.
    /// </summary>
    public class Server : IDisposable
    {
        private readonly TcpListener _listener;
        private bool _disposed;
        private bool _stopping;
        private Thread _workThread;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Server" /> class using <see cref="IPAddress.Any" /> as the ipAddress.
        /// </summary>
        /// <param name="port">The port to connect to.</param>
        public Server(int port)
            : this(IPAddress.Any, port)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Server" /> class.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="port">The port.</param>
        public Server(IPAddress ipAddress, int port)
        {
            this._listener = new TcpListener(ipAddress, port);
        }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="Server" /> is connected.
        /// </summary>
        /// <value>
        ///     <c>true</c> if connected; otherwise, <c>false</c>.
        /// </value>
        public bool Connected
        {
            get { return this._listener.Server.Connected; }
        }

        /// <summary>
        /// Gets the local endpoint for this server.
        /// </summary>
        public IPEndPoint Endpoint
        {
            get { return (IPEndPoint)this._listener.LocalEndpoint; }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Starts listening to new connections.
        /// </summary>
        /// <exception cref="InvalidOperationException">The server has already been started!</exception>
        public void Start()
        {
            if (this.Connected)
                throw new InvalidOperationException("The server has already been started!");

            this._listener.Start();
            this._workThread = new Thread(this.Work)
            {
                Name = "ByteNom.Server",
                IsBackground = true
            };
            this._workThread.Start();
        }

        /// <summary>
        ///     Stops listening to new connections.
        /// </summary>
        public void Stop()
        {
            this._stopping = true;
            this._listener.Stop();
        }

        private void Work()
        {
            while (!this._stopping)
            {
                try
                {
                    TcpClient client = this._listener.AcceptTcpClient();
                    var connection = new ServerConnection(client);
                    this.OnConnectionReceived(connection);
                    connection.Start();
                }
// ReSharper disable once EmptyGeneralCatchClause
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        ///     Occurs when a new connection is received.
        /// </summary>
        public event ConnectionEventHandler ConnectionReceived;

        /// <summary>
        ///     Called when a new connection is received.
        /// </summary>
        /// <param name="connection">The connection.</param>
        protected virtual void OnConnectionReceived(Connection connection)
        {
            ConnectionEventHandler handler = this.ConnectionReceived;
            if (handler != null) handler(this, connection);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed) return;
            this._disposed = true;

            if (disposing)
            {
                this.Stop();
            }
        }
    }
}
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ByteNom
{
    public delegate void ConnectionEventHandler(object sender, Connection connection);

    public class Server : IDisposable
    {
        private bool _stopping;
        private readonly TcpListener _listener;
        private Thread _workThread;

        public bool Connected {
            get { return this._listener.Server.Connected; }
        }

        public Server(int port) : this(IPAddress.Any, port)
        {
        }

        public Server(IPAddress ipAddress, int port)
        {
            _listener = new TcpListener(ipAddress, port);
        }

        public void Start()
        {
            if (this.Connected)
                throw new InvalidOperationException("The server has already been started!");

            this._listener.Start();
            this._workThread = new Thread(Work)
            {
                Name = "ByteNom.Server", 
                IsBackground = true
            };
            this._workThread.Start();
        }

        public void Stop()
        {
            this._stopping = true;
        }

        private void Work()
        {
            while (!_stopping)
            {
                TcpClient client = this._listener.AcceptTcpClient();
                var connection = new ServerConnection(client);
                this.OnConnection(connection);
                connection.Start();
            }
        }

        public event ConnectionEventHandler Connection;

        protected virtual void OnConnection(Connection connection)
        {
            ConnectionEventHandler handler = this.Connection;
            if (handler != null) handler(this, connection);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed) return;
            this._disposed = true;

            if (disposing)
            {
                this._stopping = true;
                this._listener.Stop();
            }
        }
    }
}
using System;
using System.Net.Sockets;
using System.Threading;
using ByteNom.Protocol;
using ProtoBuf;

namespace ByteNom
{
    /// <summary>
    ///     Represents the abstract base class for all ByteNom connections.
    /// </summary>
    public abstract class Connection : IDisposable
    {
        private bool _disposed;
        private bool _stopping;
        private Thread _thread;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Connection"/> is connected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if connected; otherwise, <c>false</c>.
        /// </value>
        public bool Connected
        {
            get { return this.Client.Connected; }
        }

        /// <summary>
        ///     Gets the tcp client.
        /// </summary>
        /// <value>
        ///     The tcp client.
        /// </value>
        protected TcpClient Client { get; private set; }

        /// <summary>
        ///     Gets the network stream.
        /// </summary>
        /// <value>
        ///     The network stream.
        /// </value>
        protected NetworkStream Stream { get; private set; }

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
        ///     Starts listening for messages.
        /// </summary>
        protected void Start()
        {
            this._thread = new Thread(this.ThreadRun)
            {
                Name = "ByteNom.Connection",
                IsBackground = true
            };
            this._thread.Start();
        }

        private void ThreadRun()
        {
            try
            {
                this.Work();
            }
// ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
            finally
            {
                this.Disconnect();
                this.OnDisconnected();
            }
        }

        /// <summary>
        ///     This method is run from the external thread of the connection. Override this to perform startup/shutdown tasks.
        /// </summary>
        protected virtual void Work()
        {
            while (!this._stopping)
            {
                var dataMsg = this.ProtoGet<DataMessage>();
                Message msg = MessageSerializer.Deserialize(dataMsg);
                this.OnMessageReceived(msg);
            }
        }

        /// <summary>
        ///     Creates and sends a message with the specified data.
        /// </summary>
        /// <param name="type">The message type.</param>
        /// <param name="args">The message arguments.</param>
        public void Send(string type, params object[] args)
        {
            this.Send(new Message(type, args));
        }

        /// <summary>
        ///     Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Send(Message message)
        {
            DataMessage dataMsg = MessageSerializer.Serialize(message);

            try
            {
                this.ProtoSend(dataMsg);
            }
// ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
        }

        /// <summary>
        ///     Disconnects from the server.
        /// </summary>
        public void Disconnect()
        {
            this.Stream.Close();
            this.Client.Close();
        }

        internal T ProtoGet<T>()
        {
            return Serializer.DeserializeWithLengthPrefix<T>(this.Stream, PrefixStyle.Base128);
        }

        internal void ProtoSend<T>(T messageObj)
        {
            Serializer.SerializeWithLengthPrefix(this.Stream, messageObj, PrefixStyle.Base128);
        }

        /// <summary>
        ///     Occurs when a message is received.
        /// </summary>
        public event MessageEventHandler MessageReceived;

        /// <summary>
        ///     Called when a message is received.
        /// </summary>
        /// <param name="message">The message.</param>
        protected virtual void OnMessageReceived(Message message)
        {
            MessageEventHandler handler = this.MessageReceived;
            if (handler != null) handler(this, message);
        }

        /// <summary>
        ///     Occurs when the connection is lost.
        /// </summary>
        public event EventHandler Disconnected;

        /// <summary>
        ///     Called when the connection is lost.
        /// </summary>
        protected virtual void OnDisconnected()
        {
            EventHandler handler = this.Disconnected;
            if (handler != null) handler(this, EventArgs.Empty);
        }    
    
        void IDisposable.Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
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
                this._stopping = true;
                this.Disconnect();
            }
        }
    }
}
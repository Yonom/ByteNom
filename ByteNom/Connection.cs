using System;
using System.Net.Sockets;
using System.Threading;
using ByteNom.Protocol;
using ProtoBuf;

namespace ByteNom
{
    /// <summary>
    /// Represents the abstract base class for all ByteNom connections.
    /// </summary>
    public abstract class Connection : IDisposable
    {
        private bool _disposed;
        private bool _stopping;
        private Thread _thread;

        /// <summary>
        /// Gets the tcp client.
        /// </summary>
        /// <value>
        /// The tcp client.
        /// </value>
        protected TcpClient Client { get; private set; }

        /// <summary>
        /// Gets the network stream.
        /// </summary>
        /// <value>
        /// The network stream.
        /// </value>
        protected NetworkStream Stream { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Starts listening to the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        protected void Start(TcpClient client)
        {
            this.Client = client;
            this.Stream = client.GetStream();
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
            catch (Exception)
            {
                // TODO: Log the error
            }
            finally
            {
                this.OnDisconnect();
            }
        }

        /// <summary>
        /// This method is run from the external thread of the connection. Override this to perform startup/shutdown tasks.
        /// </summary>
        protected virtual void Work()
        {
            while (!this._stopping)
            {
                var dataMsg = this.ProtoGet<DataMessage>();
                Message msg = MessageSerializer.Deserialize(dataMsg);
                this.OnMessage(msg);
            }
        }

        /// <summary>
        /// Creates and sends a message with the specified data.
        /// </summary>
        /// <param name="type">The message type.</param>
        /// <param name="args">The message arguments.</param>
        public void Send(string type, params object[] args)
        {
            this.Send(new Message(type, args));
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Send(Message message)
        {
            DataMessage dataMsg = MessageSerializer.Serialize(message);
            this.ProtoSend(dataMsg);
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
        /// Occurs when a message is received.
        /// </summary>
        public event MessageEventHandler Message;

        /// <summary>
        /// Called when a message is received.
        /// </summary>
        /// <param name="message">The message.</param>
        protected virtual void OnMessage(Message message)
        {
            MessageEventHandler handler = this.Message;
            if (handler != null) handler(this, message);
        }

        /// <summary>
        /// Occurs when the connection is lost.
        /// </summary>
        public event EventHandler Disconnect;

        /// <summary>
        /// Called when the connection is lost.
        /// </summary>
        protected virtual void OnDisconnect()
        {
            EventHandler handler = this.Disconnect;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed) return;
            this._disposed = true;

            if (disposing)
            {
                this._stopping = true;
                this.Stream.Close();
                this.Client.Close();
            }
        }
    }
}
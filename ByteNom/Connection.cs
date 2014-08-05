using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using ByteNom.Protocol;
using ProtoBuf;

namespace ByteNom
{
    public delegate void MessageEventHandler(object sender, Message message);

    public abstract class Connection : IDisposable
    {
        private bool _stopping;
        private Thread _thread;

        protected TcpClient Client { get; private set; }
        protected NetworkStream Stream { get; private set; }

        protected void Start(TcpClient client)
        {
            this.Client = client;
            this.Stream = client.GetStream();
            _thread = new Thread(ThreadRun)
            {
                Name = "ByteNom.Connection",
                IsBackground = true
            };
            _thread.Start();
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

        protected virtual void Work()
        {
            while (!_stopping)
            {
                var dataMsg = this.ProtoGet<DataMessage>();
                var msg = MessageSerializer.Deserialize(dataMsg);
                this.OnMessage(msg);
            }
        }

        public void Send(string type, params object[] args)
        {
            this.Send(new Message(type, args));
        }

        public void Send(Message message)
        {
            var dataMsg = MessageSerializer.Serialize(message);
            this.ProtoSend(dataMsg);
        }

        protected T ProtoGet<T>()
        {
            return Serializer.DeserializeWithLengthPrefix<T>(this.Stream, PrefixStyle.Base128);
        }

        protected void ProtoSend<T>(T messageObj)
        {
            Serializer.SerializeWithLengthPrefix(this.Stream, messageObj, PrefixStyle.Base128);
        }

        public event MessageEventHandler Message;

        protected virtual void OnMessage(Message message)
        {
            MessageEventHandler handler = this.Message;
            if (handler != null) handler(this, message);
        }
        
        public event EventHandler Disconnect;

        protected virtual void OnDisconnect()
        {
            EventHandler handler = this.Disconnect;
            if (handler != null) handler(this, EventArgs.Empty);
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
            }
        }
    }
}
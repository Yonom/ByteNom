using System;
using System.Net.Sockets;
using System.Threading;
using ByteNom.Protocol;
using ProtoBuf;

namespace ByteNom
{
    public abstract class Connection : IDisposable
    {
        private bool _disposed;
        private bool _stopping;
        private Thread _thread;

        protected TcpClient Client { get; private set; }
        protected NetworkStream Stream { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

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

        protected virtual void Work()
        {
            while (!this._stopping)
            {
                var dataMsg = this.ProtoGet<DataMessage>();
                Message msg = MessageSerializer.Deserialize(dataMsg);
                this.OnMessage(msg);
            }
        }

        public void Send(string type, params object[] args)
        {
            this.Send(new Message(type, args));
        }

        public void Send(Message message)
        {
            DataMessage dataMsg = MessageSerializer.Serialize(message);
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
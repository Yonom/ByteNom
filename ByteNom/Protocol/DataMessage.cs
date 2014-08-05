using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace ByteNom.Protocol
{
    [ProtoContract]
    internal class DataMessage
    {
        [ProtoMember(1)]
        public string Type { get; set; }

        [ProtoMember(2)]
        public DataItem[] Arguments { get; set; }

        public DataMessage()
        {
            
        }

        public DataMessage(string type, DataItem[] arguments)
        {
            this.Type = type;
            this.Arguments = arguments;
        }
    }
}

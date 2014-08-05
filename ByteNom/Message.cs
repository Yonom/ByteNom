using System;
using System.Collections.Generic;

namespace ByteNom
{
    public class Message : List<object>
    {
        public string Type { get; set; }

        public Message()
        {
        }

        public Message(string type, params object[] args)
        {
            this.Type = type;
            this.AddRange(args);
        }

        public T Get<T>(int index)
        {
            return (T)this[index];
        }

        public bool GetBool(int index)
        {
            return this.Get<bool>(index);
        }

        public int GetInt(int index)
        {
            return this.Get<int>(index);
        }

        public uint GetUInt(int index)
        {
            return this.Get<uint>(index);
        }

        public short GetShort(int index)
        {
            return this.Get<short>(index);
        }

        public ushort GetUShort(int index)
        {
            return this.Get<ushort>(index);
        }

        public long GetLong(int index)
        {
            return this.Get<long>(index);
        }

        public ulong GetULong(int index)
        {
            return this.Get<ulong>(index);
        }

        public byte GetByte(int index)
        {
            return this.Get<byte>(index);
        }

        public sbyte GetSByte(int index)
        {
            return this.Get<sbyte>(index);
        }

        public char GetChar(int index)
        {
            return this.Get<char>(index);
        }

        public string GetString(int index)
        {
            return this.Get<string>(index);
        }

        public double GetDouble(int index)
        {
            return this.Get<double>(index);
        }

        public float GetFloat(int index)
        {
            return this.Get<float>(index);
        }

        public decimal GetDecimal(int index)
        {
            return this.Get<decimal>(index);
        }

        public DateTime GetDateTime(int index)
        {
            return this.Get<DateTime>(index);
        }
    }
}

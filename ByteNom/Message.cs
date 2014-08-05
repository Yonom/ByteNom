using System;
using System.Collections.Generic;

namespace ByteNom
{
    /// <summary>
    /// The message class.
    /// </summary>
    public class Message : List<object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        public Message()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="type">The message type.</param>
        /// <param name="args">The message arguments.</param>
        public Message(string type, params object[] args)
        {
            this.Type = type;
            this.AddRange(args);
        }
        
        /// <summary>
        /// Gets or sets the message type.
        /// </summary>
        /// <value>
        /// The message type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets the argument at the specified index.
        /// </summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public T Get<T>(int index)
        {
            return (T)this[index];
        }

        /// <summary>
        /// Gets the bool at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public bool GetBool(int index)
        {
            return this.Get<bool>(index);
        }

        /// <summary>
        /// Gets the int at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public int GetInt(int index)
        {
            return this.Get<int>(index);
        }

        /// <summary>
        /// Gets the uint at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public uint GetUInt(int index)
        {
            return this.Get<uint>(index);
        }

        /// <summary>
        /// Gets the short at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public short GetShort(int index)
        {
            return this.Get<short>(index);
        }

        /// <summary>
        /// Gets the ushort at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public ushort GetUShort(int index)
        {
            return this.Get<ushort>(index);
        }

        /// <summary>
        /// Gets the long at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public long GetLong(int index)
        {
            return this.Get<long>(index);
        }

        /// <summary>
        /// Gets the ulong at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public ulong GetULong(int index)
        {
            return this.Get<ulong>(index);
        }

        /// <summary>
        /// Gets the byte at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public byte GetByte(int index)
        {
            return this.Get<byte>(index);
        }

        /// <summary>
        /// Gets the sbyte at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public sbyte GetSByte(int index)
        {
            return this.Get<sbyte>(index);
        }

        /// <summary>
        /// Gets the char at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public char GetChar(int index)
        {
            return this.Get<char>(index);
        }

        /// <summary>
        /// Gets the string at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public string GetString(int index)
        {
            return this.Get<string>(index);
        }

        /// <summary>
        /// Gets the double at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public double GetDouble(int index)
        {
            return this.Get<double>(index);
        }

        /// <summary>
        /// Gets the float at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public float GetFloat(int index)
        {
            return this.Get<float>(index);
        }

        /// <summary>
        /// Gets the decimal at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public decimal GetDecimal(int index)
        {
            return this.Get<decimal>(index);
        }

        /// <summary>
        /// Gets the DateTime at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public DateTime GetDateTime(int index)
        {
            return this.Get<DateTime>(index);
        }
    }
}
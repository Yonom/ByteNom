using System;
using System.Collections.Generic;
using System.Linq;
using ProtoBuf.Meta;

namespace ByteNom
{
    /// <summary>
    ///     Handles the Serialization of the messages.
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        ///     Registers a type. Use this method before sending custom classes.
        /// </summary>
        /// <param name="tag">
        ///     The tag. A number higher than 100 that identifies this type. Must be unique to this type and the same
        ///     on both sides of the connection.
        /// </param>
        /// <param name="type">The type.</param>
        /// <exception cref="System.ArgumentException">Tags under 100 are reserved!</exception>
        public static void RegisterType(int tag, Type type)
        {
            if (tag < 100)
                throw new ArgumentException("Tags under 100 are reserved!");

            RuntimeTypeModel.Default.Add(typeof(DataItem), true).AddSubType(tag, type);
        }

        internal static IEnumerable<DataItem> Serialize(Message message)
        {
            return message.Select(DataItem.CreateDynamic);
        }
    }
}
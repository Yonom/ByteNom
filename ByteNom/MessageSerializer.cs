using System;
using System.Linq;
using ByteNom.Protocol;
using ProtoBuf.Meta;

namespace ByteNom
{
    /// <summary>
    ///     Handles the Serialization of the messages.
    /// </summary>
    public static class MessageSerializer
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

            RuntimeTypeModel.Default.Add(typeof(DataItem), true)
                .AddSubType(tag, typeof(DataItem<>).MakeGenericType(type));
        }

        internal static DataMessage Serialize(Message message)
        {
            return new DataMessage(
                message.Type,
                message.Select(DataItem.CreateDynamic).ToArray());
        }

        internal static Message Deserialize(DataMessage message)
        {
            // If there are no arguments
            if (message.Arguments == null)
                return new Message(message.Type);

            return new Message(message.Type, message.Arguments.Select(di =>
            {
                // Handle nested messages
                var dataItem = di as DataItem<DataMessage>;
                if (dataItem != null)
                    return Deserialize(dataItem.Value);

                return di.Value;
            }).ToArray());
        }
    }
}
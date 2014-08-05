using System;
using ProtoBuf.Meta;

namespace ByteNom
{
    public static class Serializer
    {
        public static void RegisterType(int tag, Type type)
        {
            RuntimeTypeModel.Default.Add(typeof(DataItem), true).AddSubType(tag, type);
        }
    }
}

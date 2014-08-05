using System;
using ProtoBuf;

namespace ByteNom.Protocol
{
    [ProtoContract]
    internal abstract partial class DataItem
    {
        public object Value
        {
            get { return this.ValueImpl; }
            set { this.ValueImpl = value; }
        }

        protected abstract object ValueImpl { get; set; }

        public static DataItem<T> Create<T>(T value)
        {
            return new DataItem<T>(value);
        }

        public static DataItem CreateDynamic(object value)
        {
            Type type = value.GetType();
            switch (Type.GetTypeCode(value.GetType()))
            {
                    // special cases
                case TypeCode.Boolean:
                    return Create((bool)value);
                case TypeCode.Byte:
                    return Create((byte)value);
                case TypeCode.Char:
                    return Create((char)value);
                case TypeCode.DateTime:
                    return Create((DateTime)value);
                case TypeCode.Decimal:
                    return Create((decimal)value);
                case TypeCode.Double:
                    return Create((double)value);
                case TypeCode.Int16:
                    return Create((short)value);
                case TypeCode.Int32:
                    return Create((int)value);
                case TypeCode.Int64:
                    return Create((long)value);
                case TypeCode.SByte:
                    return Create((sbyte)value);
                case TypeCode.Single:
                    return Create((float)value);
                case TypeCode.String:
                    return Create((string)value);
                case TypeCode.UInt16:
                    return Create((ushort)value);
                case TypeCode.UInt32:
                    return Create((uint)value);
                case TypeCode.UInt64:
                    return Create((ulong)value);
                case TypeCode.Empty:
                    throw new ArgumentNullException("value");
                case TypeCode.Object:
                    var param = (DataItem)Activator.CreateInstance(
                        typeof(DataItem<>).MakeGenericType(type));
                    param.Value = value;
                    return param;

                default:
                    throw new ArgumentException("The given value is not supported!");
            }
        }
    }

    [ProtoContract]
    internal sealed class DataItem<T> : DataItem
    {
        public DataItem()
        {
        }

        public DataItem(T value)
        {
            this.Value = value;
        }

        [ProtoMember(1)]
        public new T Value { get; set; }

        protected override object ValueImpl
        {
            get { return this.Value; }
            set { this.Value = (T)value; }
        }
    }
}
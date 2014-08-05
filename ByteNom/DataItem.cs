using ProtoBuf;

namespace ByteNom
{
    [ProtoContract]
    internal abstract partial class DataItem
    {
        public static DataItem<T> Create<T>(T value)
        {
            return new DataItem<T>(value);
        }

        public object Value
        {
            get { return ValueImpl; }
            set { ValueImpl = value; }
        }

        protected abstract object ValueImpl { get; set; }
    }

    [ProtoContract]
    internal sealed class DataItem<T> : DataItem
    {
        public DataItem()
        {
        }

        public DataItem(T value)
        {
            Value = value;
        }

        [ProtoMember(1)]
        public new T Value { get; set; }

        protected override object ValueImpl
        {
            get { return Value; }
            set { Value = (T)value; }
        }
    }
}

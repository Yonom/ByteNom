using System;
using ProtoBuf;

namespace ByteNom.Protocol
{
    [
        // Basic classes
		ProtoInclude(5, typeof(DataItem<string>)),
		ProtoInclude(6, typeof(DataItem<int>)),
		ProtoInclude(7, typeof(DataItem<bool>)),
		ProtoInclude(8, typeof(DataItem<double>)),
		ProtoInclude(9, typeof(DataItem<DataMessage>)),
		ProtoInclude(10, typeof(DataItem<uint>)),
		ProtoInclude(11, typeof(DataItem<short>)),
		ProtoInclude(12, typeof(DataItem<ushort>)),
		ProtoInclude(13, typeof(DataItem<long>)),
		ProtoInclude(14, typeof(DataItem<ulong>)),
		ProtoInclude(15, typeof(DataItem<byte>)),
		ProtoInclude(16, typeof(DataItem<sbyte>)),
		ProtoInclude(17, typeof(DataItem<char>)),
		ProtoInclude(18, typeof(DataItem<float>)),
		ProtoInclude(19, typeof(DataItem<decimal>)),
		ProtoInclude(20, typeof(DataItem<DateTime>)),
        
        // Arrays
		ProtoInclude(21, typeof(DataItem<string[]>)),
		ProtoInclude(22, typeof(DataItem<int[]>)),
		ProtoInclude(23, typeof(DataItem<bool[]>)),
		ProtoInclude(24, typeof(DataItem<double[]>)),
		ProtoInclude(25, typeof(DataItem<DataMessage[]>)),
		ProtoInclude(26, typeof(DataItem<uint[]>)),
		ProtoInclude(27, typeof(DataItem<short[]>)),
		ProtoInclude(28, typeof(DataItem<ushort[]>)),
		ProtoInclude(29, typeof(DataItem<long[]>)),
		ProtoInclude(30, typeof(DataItem<ulong[]>)),
		ProtoInclude(31, typeof(DataItem<byte[]>)),
		ProtoInclude(32, typeof(DataItem<sbyte[]>)),
		ProtoInclude(33, typeof(DataItem<char[]>)),
		ProtoInclude(34, typeof(DataItem<float[]>)),
		ProtoInclude(35, typeof(DataItem<decimal[]>)),
		ProtoInclude(36, typeof(DataItem<DateTime[]>)),
    ]
    partial class DataItem
	{
    }
}
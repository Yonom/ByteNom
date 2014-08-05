using System;
using ProtoBuf;
using System.Collections.Generic;

namespace ByteNom
{
    [
        // Basic classes
		ProtoInclude(5, typeof(DataItem<string>)),
		ProtoInclude(6, typeof(DataItem<int>)),
		ProtoInclude(7, typeof(DataItem<bool>)),
		ProtoInclude(8, typeof(DataItem<double>)),
		ProtoInclude(9, typeof(DataItem<uint>)),
		ProtoInclude(10, typeof(DataItem<short>)),
		ProtoInclude(11, typeof(DataItem<ushort>)),
		ProtoInclude(12, typeof(DataItem<long>)),
		ProtoInclude(13, typeof(DataItem<ulong>)),
		ProtoInclude(14, typeof(DataItem<byte>)),
		ProtoInclude(15, typeof(DataItem<sbyte>)),
		ProtoInclude(16, typeof(DataItem<char>)),
		ProtoInclude(17, typeof(DataItem<float>)),
		ProtoInclude(18, typeof(DataItem<decimal>)),
		ProtoInclude(19, typeof(DataItem<DateTime>)),
        
        // Lists
		ProtoInclude(20, typeof(DataItem<List<string>>)),
		ProtoInclude(21, typeof(DataItem<List<int>>)),
		ProtoInclude(22, typeof(DataItem<List<bool>>)),
		ProtoInclude(23, typeof(DataItem<List<double>>)),
		ProtoInclude(24, typeof(DataItem<List<uint>>)),
		ProtoInclude(25, typeof(DataItem<List<short>>)),
		ProtoInclude(26, typeof(DataItem<List<ushort>>)),
		ProtoInclude(27, typeof(DataItem<List<long>>)),
		ProtoInclude(28, typeof(DataItem<List<ulong>>)),
		ProtoInclude(29, typeof(DataItem<List<byte>>)),
		ProtoInclude(30, typeof(DataItem<List<sbyte>>)),
		ProtoInclude(31, typeof(DataItem<List<char>>)),
		ProtoInclude(32, typeof(DataItem<List<float>>)),
		ProtoInclude(33, typeof(DataItem<List<decimal>>)),
		ProtoInclude(34, typeof(DataItem<List<DateTime>>)),
        
        // Arrays
		ProtoInclude(35, typeof(DataItem<string[]>)),
		ProtoInclude(36, typeof(DataItem<int[]>)),
		ProtoInclude(37, typeof(DataItem<bool[]>)),
		ProtoInclude(38, typeof(DataItem<double[]>)),
		ProtoInclude(39, typeof(DataItem<uint[]>)),
		ProtoInclude(40, typeof(DataItem<short[]>)),
		ProtoInclude(41, typeof(DataItem<ushort[]>)),
		ProtoInclude(42, typeof(DataItem<long[]>)),
		ProtoInclude(43, typeof(DataItem<ulong[]>)),
		ProtoInclude(44, typeof(DataItem<byte[]>)),
		ProtoInclude(45, typeof(DataItem<sbyte[]>)),
		ProtoInclude(46, typeof(DataItem<char[]>)),
		ProtoInclude(47, typeof(DataItem<float[]>)),
		ProtoInclude(48, typeof(DataItem<decimal[]>)),
		ProtoInclude(49, typeof(DataItem<DateTime[]>)),
    ]
    partial class DataItem
	{
    }
}
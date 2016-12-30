using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class Packer_Thrift:IPacker
{
    public byte[] DoPack(PackDataStruct Data)
    {
        Clear();
        EncodeStruct(Data);
        return GetBuffer();
    }

    #region expacker
    private ByteStream trans;
    public byte[] GetBuffer()
    {
        return trans.GetBuffer();
    }
    public void EncodeStruct(PackDataStruct data)
    {
        WriteStructBegin();

        for (int i = 0; i < data.m_ElemList.Count; ++i)
        {
            var elem = data.m_ElemList[i];
            WriteFieldBegin(GetFiledByDesc(elem));
            EncodeDataElement(elem);
            WriteFieldEnd();
        }

        WriteFieldStop();
        WriteStructEnd();
    }
    private TField GetFiledByDesc(PackDataElement data)
    {
        TField res = new TField();
        res.Name = data.m_strName;
        res.ID = data.m_Id;
        switch (data.m_Type)
        {
            case PackDataElementType.Bool:
                res.Type = TType.Bool;
                break;
            case PackDataElementType.Byte:
                res.Type = TType.Byte;
                break;
            case PackDataElementType.Double:
                res.Type = TType.Double;
                break;
            case PackDataElementType.I16:
                res.Type = TType.I16;
                break;
            case PackDataElementType.I32:
                res.Type = TType.I32;
                break;
            case PackDataElementType.I64:
                res.Type = TType.I64;
                break;
            case PackDataElementType.String:
                res.Type = TType.String;
                break;
            case PackDataElementType.Struct:
                res.Type = TType.Struct;
                break;
            case PackDataElementType.Map:
                res.Type = TType.Map;
                break;
            case PackDataElementType.Set:
                res.Type = TType.Set;
                break;
            case PackDataElementType.List:
                res.Type = TType.List;
                break;
            default:
                {
                    throw new Exception("Error type");
                }
        }
        return res;
    }
    private void EncodeBool(PackDataElement data)
    {
        WriteBool((bool)data.m_Value);
    }
    private void EncodeString(PackDataElement elem)
    {
        WriteString((string)elem.m_Value);
    }
    private void EncodeI64(PackDataElement elem)
    {
        WriteI64((long)elem.m_Value);
    }
    private void EncodeI32(PackDataElement elem)
    {
        WriteI32((int)elem.m_Value);
    }
    private void EncodeI16(PackDataElement elem)
    {
        WriteI16((short)elem.m_Value);
    }
    private void EncodeDouble(PackDataElement elem)
    {
        WriteDouble((double)elem.m_Value);
    }
    private void EncodeByte(PackDataElement elem)
    {
        WriteByteDirect((int)elem.m_Value);
    }
    private void EncodeList(PackDataElement elem)
    {
        List<PackDataElement> list = elem.m_Value as List<PackDataElement>;
        if (null == list || list.Count == 0)
        {
            WriteListBegin(TType.I32, 0);
        }
        else
        {
            var filed = GetFiledByDesc(list[0]);
            WriteListBegin(filed.Type, list.Count);
            var targetType = list[0].m_Type;

            for (int i = 0; i < list.Count; ++i)
            {
                var subElem = list[i];
                if (subElem.m_Type != targetType)
                {
                    throw new Exception("error type in same list");
                }
                EncodeDataElement(subElem);
            }
        }

        WriteListEnd();
    }
    private void EncodeSet(PackDataElement elem)
    {
        HashSet<PackDataElement> set = elem.m_Value as HashSet<PackDataElement>;
        if (null == set || set.Count == 0)
        {
            WriteSetBegin(TType.I32, 0);
        }
        else
        {
            var filed = GetFiledByDesc(set.First());
            WriteSetBegin(filed.Type, set.Count);
            var targetType = set.First().m_Type;
            foreach (var subElem in set)
            {
                if (subElem.m_Type != targetType)
                {
                    throw new Exception("error type in same list");
                }
                EncodeDataElement(subElem);
            }
        }
        WriteSetEnd();
    }
    private void EncodeMap(PackDataElement elem)
    {
        Dictionary<PackDataElement, PackDataElement> map = elem.m_Value as Dictionary<PackDataElement, PackDataElement>;
        if (null == map || map.Count == 0)
        {
            WriteMapBegin(TType.I32, TType.I32, 0);
        }
        else
        {
            var keyFiled = GetFiledByDesc(map.First().Key);
            var targetKeyType = map.First().Key.m_Type;

            var vlaueFiled = GetFiledByDesc(map.First().Value);
            var targetValueType = map.First().Value.m_Type;
            WriteMapBegin(keyFiled.Type, vlaueFiled.Type,map.Count);

            foreach (var tmpElem in map)
            {
                PackDataElement subKeyElem = tmpElem.Key;
                PackDataElement subValueElem = tmpElem.Value;

                if (subKeyElem.m_Type != targetKeyType)
                {
                    throw new Exception("error type in same list");
                }
                EncodeDataElement(subKeyElem);

                if (subValueElem.m_Type != targetValueType)
                {
                    throw new Exception("error type in same list");
                }
                EncodeDataElement(subValueElem);
            }
        }
        WriteSetEnd();
    }
    private void EncodeDataElement(PackDataElement elem)
    {
        switch (elem.m_Type)
        {
            case PackDataElementType.Bool:
                EncodeBool(elem);
                break;
            case PackDataElementType.Byte:
                EncodeByte(elem);
                break;
            case PackDataElementType.Double:
                EncodeDouble(elem);
                break;
            case PackDataElementType.I16:
                EncodeI16(elem);
                break;
            case PackDataElementType.I32:
                EncodeI32(elem);
                break;
            case PackDataElementType.I64:
                EncodeI64(elem);
                break;
            case PackDataElementType.String:
                EncodeString(elem);
                break;
            case PackDataElementType.Struct:
                EncodeStruct(elem.m_Value as PackDataStruct);
                break;
            case PackDataElementType.Map:
                EncodeMap(elem);
                break;
            case PackDataElementType.Set:
                EncodeSet(elem);
                break;
            case PackDataElementType.List:
                EncodeList(elem);
                break;
            default:
                {
                    throw new Exception("Error type");
                }
        }
    }
    #endregion

    #region base packer tool
    /** 
         * Used to keep track of the last field for the current and previous structs,
         * so we can do the delta stuff.
         */
    //private Stack<short> lastField_ = new Stack<short>(15);

    //private short lastFieldId_ = 0;
    private const byte PROTOCOL_ID = 0x82;
    private const byte VERSION = 1;
    private const byte VERSION_MASK = 0x1f; // 0001 1111
    private const byte TYPE_MASK = 0xE0; // 1110 0000
    private const int TYPE_SHIFT_AMOUNT = 5;

    public struct TField
    {
        private string name;
        private TType type;
        private short id;

        public TField(string name, TType type, short id)
            : this()
        {
            this.name = name;
            this.type = type;
            this.id = id;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public TType Type
        {
            get { return type; }
            set { type = value; }
        }

        public short ID
        {
            get { return id; }
            set { id = value; }
        }
    }
    private static byte[] ttypeToCompactType = new byte[16];
    public enum TType : byte
    {
        Stop = 0,
        Void = 1,
        Bool = 2,
        Byte = 3,
        Double = 4,
        I16 = 6,
        I32 = 8,
        I64 = 10,
        String = 11,
        Struct = 12,
        Map = 13,
        Set = 14,
        List = 15
    }
    private static class Types
    {
        public const byte STOP = 0x00;
        public const byte BOOLEAN_TRUE = 0x01;
        public const byte BOOLEAN_FALSE = 0x02;
        public const byte BYTE = 0x03;
        public const byte I16 = 0x04;
        public const byte I32 = 0x05;
        public const byte I64 = 0x06;
        public const byte DOUBLE = 0x07;
        public const byte BINARY = 0x08;
        public const byte LIST = 0x09;
        public const byte SET = 0x0A;
        public const byte MAP = 0x0B;
        public const byte STRUCT = 0x0C;
    }
    public Packer_Thrift()
    {
        trans = new ByteStream();
        ttypeToCompactType[(int)TType.Stop] = Types.STOP;
        ttypeToCompactType[(int)TType.Bool] = Types.BOOLEAN_TRUE;
        ttypeToCompactType[(int)TType.Byte] = Types.BYTE;
        ttypeToCompactType[(int)TType.I16] = Types.I16;
        ttypeToCompactType[(int)TType.I32] = Types.I32;
        ttypeToCompactType[(int)TType.I64] = Types.I64;
        ttypeToCompactType[(int)TType.Double] = Types.DOUBLE;
        ttypeToCompactType[(int)TType.String] = Types.BINARY;
        ttypeToCompactType[(int)TType.List] = Types.LIST;
        ttypeToCompactType[(int)TType.Set] = Types.SET;
        ttypeToCompactType[(int)TType.Map] = Types.MAP;
        ttypeToCompactType[(int)TType.Struct] = Types.STRUCT;
    }
    public void Clear()
    {
        trans.Clear();
    }
    /**
        * Write a struct begin. This doesn't actually put anything on the wire. We 
        * use it as an opportunity to put special placeholder markers on the field
        * stack so we can get the field id deltas correct.
        */
    public void WriteStructBegin()
    {
        //lastField_.Push(lastFieldId_);
        //lastFieldId_ = 0;
    }
    /**
     * Write a struct end. This doesn't actually put anything on the wire. We use
     * this as an opportunity to pop the last field from the current struct off
     * of the field stack.
     */
    public void WriteStructEnd()
    {
        //lastFieldId_ = lastField_.Pop();
    }
    /**
         * Write a field header containing the field id and field type. If the
         * difference between the current field id and the last one is small (< 15),
         * then the field id will be encoded in the 4 MSB as a delta. Otherwise, the
         * field id will follow the type header as a zigzag varint.
         */
    private TField? booleanField_;
    public void WriteFieldBegin(TField field)
    {
        if (field.Type == TType.Bool)
        {
            // we want to possibly include the value, so we'll wait.
            booleanField_ = field;
        }
        else
        {
            WriteFieldBeginInternal(field, 0xFF);
        }
    }
    /**
     * The workhorse of WriteFieldBegin. It has the option of doing a 
     * 'type override' of the type header. This is used specifically in the 
     * boolean field case.
     */
    private void WriteFieldBeginInternal(TField field, byte typeOverride)
    {
        // short lastField = lastField_.Pop();

        // if there's a type override, use that.
        byte typeToWrite = typeOverride == 0xFF ? getCompactType(field.Type) : typeOverride;

        // check if we can use delta encoding for the field id
        //if (field.ID > lastFieldId_ && field.ID - lastFieldId_ <= 15)
        //{
        // Write them together
        //   WriteByteDirect((field.ID - lastFieldId_) << 4 | typeToWrite);
        // }
        //else
        {
            //Write them separate
            WriteByteDirect(typeToWrite);
            WriteI16(field.ID);
        }

        //lastFieldId_ = field.ID;
        // lastField_.push(field.id);
    }
    /**
     * Write the STOP symbol so we know there are no more fields in this struct.
     */
    public void WriteFieldStop()
    {
        WriteByteDirect(Types.STOP);
    }
    /**
        * Given a TType value, find the appropriate TCompactProtocol.Types constant.
        */
    private byte getCompactType(TType ttype)
    {
        return ttypeToCompactType[(int)ttype];
    }
    /** 
        * Writes a byte without any possibility of all that field header nonsense. 
        * Used internally by other writing methods that know they need to Write a byte.
        */
    private byte[] byteDirectBuffer = new byte[1];
    private byte WriteByteDirect(byte b)
    {
        byteDirectBuffer[0] = b;
        trans.Write(byteDirectBuffer);
        return b;
    }
    /** 
     * Writes a byte without any possibility of all that field header nonsense.
     */
    private void WriteByteDirect(int n)
    {
        WriteByteDirect((byte)n);
    }
    /**
         * Abstract method for writing the start of lists and sets. List and sets on 
         * the wire differ only by the type indicator.
         */
    protected void WriteCollectionBegin(TType elemType, int size)
    {
        if (size <= 14)
        {
            WriteByteDirect(size << 4 | getCompactType(elemType));
        }
        else
        {
            WriteByteDirect(0xf0 | getCompactType(elemType));
            WriteVarint32((uint)size);
        }
    }
    byte[] i32buf = new byte[5];
    private void WriteVarint32(uint n)
    {
        int idx = 0;
        while (true)
        {
            if ((n & ~0x7F) == 0)
            {
                i32buf[idx++] = (byte)n;
                // WriteByteDirect((byte)n);
                break;
                // return;
            }
            else
            {
                i32buf[idx++] = (byte)((n & 0x7F) | 0x80);
                // WriteByteDirect((byte)((n & 0x7F) | 0x80));
                n >>= 7;
            }
        }
        trans.Write(i32buf, 0, idx);
    }
    /**
     * Write an i64 as a varint. Results in 1-10 bytes on the wire.
     */
    byte[] varint64out = new byte[10];
    private void WriteVarint64(ulong n)
    {
        int idx = 0;
        while (true)
        {
            if ((n & ~(ulong)0x7FL) == 0)
            {
                varint64out[idx++] = (byte)n;
                break;
            }
            else
            {
                varint64out[idx++] = ((byte)((n & 0x7F) | 0x80));
                n >>= 7;
            }
        }
        trans.Write(varint64out, 0, idx);
    }
    public void WriteI16(short i16)
    {
        WriteVarint32(intToZigZag(i16));
    }
    /**
     * Write an i32 as a zigzag varint.
     */
    public void WriteI32(int i32)
    {
        WriteVarint32(intToZigZag(i32));
    }
    /**
     * Write an i64 as a zigzag varint.
     */
    public void WriteI64(long i64)
    {
        WriteVarint64(longToZigzag(i64));
    }
    /**
         * Write a double to the wire as 8 bytes.
         */
    public void WriteDouble(double dub)
    {
        byte[] data = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        fixedLongToBytes(BitConverter.DoubleToInt64Bits(dub), data, 0);
        trans.Write(data);
    }

    /**
     * Write a string to the wire with a varint size preceding.
     */
    public void WriteString(String str)
    {
        byte[] bytes = UTF8Encoding.UTF8.GetBytes(str);
        WriteBinary(bytes, 0, bytes.Length);
    }
    /**
     * Write a byte array, using a varint for the size. 
     */
    public void WriteBinary(byte[] bin)
    {
        WriteBinary(bin, 0, bin.Length);
    }
    private void WriteBinary(byte[] buf, int offset, int length)
    {
        WriteVarint32((uint)length);
        trans.Write(buf, offset, length);
    }
    /**
        * Write a boolean value. Potentially, this could be a boolean field, in 
        * which case the field header info isn't written yet. If so, decide what the
        * right type header is for the value and then Write the field header. 
        * Otherwise, Write a single byte.
        */
    public void WriteBool(Boolean b)
    {
        if (booleanField_ != null)
        {
            // we haven't written the field header yet
            WriteFieldBeginInternal(booleanField_.Value, b ? Types.BOOLEAN_TRUE : Types.BOOLEAN_FALSE);
            booleanField_ = null;
        }
        else
        {
            // we're not part of a field, so just Write the value.
            WriteByteDirect(b ? Types.BOOLEAN_TRUE : Types.BOOLEAN_FALSE);
        }
    }/**
         * Write a map header. If the map is empty, omit the key and value type 
         * headers, as we don't need any additional information to skip it.
         */
    public void WriteMapBegin(TType keyType, TType valueType, int size)
    {
        if (size == 0)
        {
            WriteByteDirect(0);
        }
        else
        {
            WriteVarint32((uint)size);
            WriteByteDirect(getCompactType(keyType) << 4 | getCompactType(valueType));
        }
    }
    /** 
     * Write a list header.
     */
    public void WriteListBegin(TType type, int size)
    {
        WriteCollectionBegin(type, size);
    }

    /**
     * Write a set header.
     */
    public void WriteSetBegin(TType type, int size)
    {
        WriteCollectionBegin(type, size);
    }
    /**
     * Convert l into a zigzag long. This allows negative numbers to be 
     * represented compactly as a varint.
     */
    private ulong longToZigzag(long n)
    {
        return (ulong)(n << 1) ^ (ulong)(n >> 63);
    }
    /**
     * Convert n into a zigzag int. This allows negative numbers to be 
     * represented compactly as a varint.
     */
    private uint intToZigZag(int n)
    {
        return (uint)(n << 1) ^ (uint)(n >> 31);
    }
    /**
     * Convert a long into little-endian bytes in buf starting at off and going 
     * until off+7.
     */
    private void fixedLongToBytes(long n, byte[] buf, int off)
    {
        buf[off + 0] = (byte)(n & 0xff);
        buf[off + 1] = (byte)((n >> 8) & 0xff);
        buf[off + 2] = (byte)((n >> 16) & 0xff);
        buf[off + 3] = (byte)((n >> 24) & 0xff);
        buf[off + 4] = (byte)((n >> 32) & 0xff);
        buf[off + 5] = (byte)((n >> 40) & 0xff);
        buf[off + 6] = (byte)((n >> 48) & 0xff);
        buf[off + 7] = (byte)((n >> 56) & 0xff);
    }
    public void WriteMessageEnd() { }
    public void WriteMapEnd() { }
    public void WriteListEnd() { }
    public void WriteSetEnd() { }
    public void WriteFieldEnd() { }
    #endregion
}

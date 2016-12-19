using System;
using System.Collections.Generic;

public class ByteStream
{
    private List<byte> buffer;

    public ByteStream()
    {
        buffer = new List<byte>(64);
    }
    public void Clear()
    {
        buffer.Clear();
    }
    public void Write(byte[] buf)
    {
        buffer.AddRange(buf);
    }
    public void Write(byte[] buf, int offset, int length)
    {
        int size = offset + length;
        for (int i = offset; i < size; ++i)
        {
            buffer.Add(buf[i]);
        }
    }
    public byte[] GetBuffer()
    {
        return buffer.ToArray();
    }
    public byte[] Read(int offset, int size)
    {
        byte[] source = buffer.ToArray();
        byte[] res = new byte[size];
        Array.Copy(source, offset, res, 0, size);
        return res;
    }
}
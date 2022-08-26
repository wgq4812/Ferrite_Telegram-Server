//  <auto-generated>
//  This file was auto-generated by the Ferrite TL Generator.
//  Please do not modify as all changes will be lost.
//  <auto-generated/>

#nullable enable

using System.Buffers;
using System.Runtime.InteropServices;
using Ferrite.Utils;

namespace Ferrite.TL.slim.mtproto;

public readonly ref struct msg_container
{
    private readonly Span<byte> _buff;
    public msg_container(Span<byte> buff)
    {
        _buff = buff;
    }
    
    public readonly int Constructor => MemoryMarshal.Read<int>(_buff);

    private void SetConstructor(int constructor)
    {
        MemoryMarshal.Write(_buff.Slice(0, 4), ref constructor);
    }
    public int Length => _buff.Length;
    public ReadOnlySpan<byte> ToReadOnlySpan() => _buff;
    public static Span<byte> Read(Span<byte> data, int offset)
    {
        var bytesRead = GetOffset(2, data[offset..]);
        if (bytesRead > data.Length + offset)
        {
            return Span<byte>.Empty;
        }
        return data.Slice(offset, bytesRead);
    }

    public static int GetRequiredBufferSize(int len_messages)
    {
        return 4 + len_messages;
    }
    public static msg_container Create(VectorBare messages, out IMemoryOwner<byte> memory, MemoryPool<byte>? pool = null)
    {
        var length = GetRequiredBufferSize(messages.Length);
        memory = pool != null ? pool.Rent(length) : MemoryPool<byte>.Shared.Rent(length);
        memory.Memory.Span.Clear();
        var obj = new msg_container(memory.Memory.Span[..length]);
        obj.SetConstructor(unchecked((int)0x73f1f8dc));
        obj.Set_messages(messages.ToReadOnlySpan());
        return obj;
    }
    public static int ReadSize(Span<byte> data, int offset)
    {
        return GetOffset(2, data[offset..]);
    }
    public VectorBare messages => new VectorBare(_buff.Slice(GetOffset(1, _buff)));
    private void Set_messages(ReadOnlySpan<byte> value)
    {
        value.CopyTo(_buff[GetOffset(1, _buff)..]);
    }
    private static int GetOffset(int index, Span<byte> buffer)
    {
        int offset = 4;
        if(index >= 2) offset += VectorBare.ReadSize(buffer, offset);
        return offset;
    }
}

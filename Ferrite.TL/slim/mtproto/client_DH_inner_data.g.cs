//  <auto-generated>
//  This file was auto-generated by the Ferrite TL Generator.
//  Please do not modify as all changes will be lost.
//  <auto-generated/>

#nullable enable

using System.Buffers;
using System.Runtime.InteropServices;
using Ferrite.Utils;

namespace Ferrite.TL.slim.mtproto;

public readonly ref struct client_DH_inner_data
{
    private readonly Span<byte> _buff;
    public client_DH_inner_data(Span<byte> buff)
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
        var bytesRead = GetOffset(5, data[offset..]);
        if (bytesRead > data.Length + offset)
        {
            return Span<byte>.Empty;
        }
        return data.Slice(offset, bytesRead);
    }

    public static int GetRequiredBufferSize(int len_g_b)
    {
        return 4 + 16 + 16 + 8 + BufferUtils.CalculateTLBytesLength(len_g_b);
    }
    public static client_DH_inner_data Create(ReadOnlySpan<byte> nonce, ReadOnlySpan<byte> server_nonce, long retry_id, ReadOnlySpan<byte> g_b, out IMemoryOwner<byte> memory, MemoryPool<byte>? pool = null)
    {
        var length = GetRequiredBufferSize(g_b.Length);
        memory = pool != null ? pool.Rent(length) : MemoryPool<byte>.Shared.Rent(length);
        memory.Memory.Span.Clear();
        var obj = new client_DH_inner_data(memory.Memory.Span[..length]);
        obj.SetConstructor(unchecked((int)0x6643b654));
        obj.Set_nonce(nonce);
        obj.Set_server_nonce(server_nonce);
        obj.Set_retry_id(retry_id);
        obj.Set_g_b(g_b);
        return obj;
    }
    public static int ReadSize(Span<byte> data, int offset)
    {
        return GetOffset(5, data[offset..]);
    }
    public ReadOnlySpan<byte> nonce => _buff.Slice(GetOffset(1, _buff), 16);
    private void Set_nonce(ReadOnlySpan<byte> value)
    {
        if(value.Length != 16)
        {
            return;
        }
        value.CopyTo(_buff.Slice(GetOffset(1, _buff), 16));
    }
    public ReadOnlySpan<byte> server_nonce => _buff.Slice(GetOffset(2, _buff), 16);
    private void Set_server_nonce(ReadOnlySpan<byte> value)
    {
        if(value.Length != 16)
        {
            return;
        }
        value.CopyTo(_buff.Slice(GetOffset(2, _buff), 16));
    }
    public readonly long retry_id => MemoryMarshal.Read<long>(_buff[GetOffset(3, _buff)..]);
    private void Set_retry_id(long value)
    {
        MemoryMarshal.Write(_buff[GetOffset(3, _buff)..], ref value);
    }
    public ReadOnlySpan<byte> g_b => BufferUtils.GetTLBytes(_buff, GetOffset(4, _buff));
    private void Set_g_b(ReadOnlySpan<byte> value)
    {
        if(value.Length == 0)
        {
            return;
        }
        var offset = GetOffset(4, _buff);
        var lenBytes = BufferUtils.WriteLenBytes(_buff, value, offset);
        if(_buff.Length < offset + lenBytes + value.Length) return;
        value.CopyTo(_buff[(offset + lenBytes)..]);
    }
    private static int GetOffset(int index, Span<byte> buffer)
    {
        int offset = 4;
        if(index >= 2) offset += 16;
        if(index >= 3) offset += 16;
        if(index >= 4) offset += 8;
        if(index >= 5) offset += BufferUtils.GetTLBytesLength(buffer, offset);
        return offset;
    }
}

//  <auto-generated>
//  This file was auto-generated by the Ferrite TL Generator.
//  Please do not modify as all changes will be lost.
//  <auto-generated/>

#nullable enable

using System.Buffers;
using System.Runtime.InteropServices;
using Ferrite.Utils;

namespace Ferrite.TL.slim.mtproto;

public readonly ref struct server_DH_params_ok
{
    private readonly Span<byte> _buff;
    public server_DH_params_ok(Span<byte> buff)
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
        var bytesRead = GetOffset(4, data[offset..]);
        if (bytesRead > data.Length + offset)
        {
            return Span<byte>.Empty;
        }
        return data.Slice(offset, bytesRead);
    }

    public static int GetRequiredBufferSize(int len_encrypted_answer)
    {
        return 4 + 16 + 16 + BufferUtils.CalculateTLBytesLength(len_encrypted_answer);
    }
    public static server_DH_params_ok Create(ReadOnlySpan<byte> nonce, ReadOnlySpan<byte> server_nonce, ReadOnlySpan<byte> encrypted_answer, out IMemoryOwner<byte> memory, MemoryPool<byte>? pool = null)
    {
        var length = GetRequiredBufferSize(encrypted_answer.Length);
        memory = pool != null ? pool.Rent(length) : MemoryPool<byte>.Shared.Rent(length);
        memory.Memory.Span.Clear();
        var obj = new server_DH_params_ok(memory.Memory.Span[..length]);
        obj.SetConstructor(unchecked((int)0xd0e8075c));
        obj.Set_nonce(nonce);
        obj.Set_server_nonce(server_nonce);
        obj.Set_encrypted_answer(encrypted_answer);
        return obj;
    }
    public static int ReadSize(Span<byte> data, int offset)
    {
        return GetOffset(4, data[offset..]);
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
    public ReadOnlySpan<byte> encrypted_answer => BufferUtils.GetTLBytes(_buff, GetOffset(3, _buff));
    private void Set_encrypted_answer(ReadOnlySpan<byte> value)
    {
        if(value.Length == 0)
        {
            return;
        }
        var offset = GetOffset(3, _buff);
        var lenBytes = BufferUtils.WriteLenBytes(_buff, value, offset);
        if(_buff.Length < offset + lenBytes + value.Length) return;
        value.CopyTo(_buff[(offset + lenBytes)..]);
    }
    private static int GetOffset(int index, Span<byte> buffer)
    {
        int offset = 4;
        if(index >= 2) offset += 16;
        if(index >= 3) offset += 16;
        if(index >= 4) offset += BufferUtils.GetTLBytesLength(buffer, offset);
        return offset;
    }
}

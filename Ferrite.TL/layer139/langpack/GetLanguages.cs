/*
 *   Project Ferrite is an Implementation Telegram Server API
 *   Copyright 2022 Aykut Alparslan KOC <aykutalparslan@msn.com>
 *
 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU Affero General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU Affero General Public License for more details.
 *
 *   You should have received a copy of the GNU Affero General Public License
 *   along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Buffers;
using DotNext.Buffers;
using DotNext.IO;
using Ferrite.Services;
using Ferrite.TL.mtproto;
using Ferrite.Utils;

namespace Ferrite.TL.layer139.langpack;
public class GetLanguages : ITLObject, ITLMethod
{
    private readonly SparseBufferWriter<byte> writer = new SparseBufferWriter<byte>(UnmanagedMemoryPool<byte>.Shared);
    private readonly ITLObjectFactory factory;
    private readonly ILangPackService _langPackService;
    private bool serialized = false;
    public GetLanguages(ITLObjectFactory objectFactory, ILangPackService langPackService)
    {
        factory = objectFactory;
        _langPackService = langPackService;
    }

    public int Constructor => 1120311183;
    public ReadOnlySequence<byte> TLBytes
    {
        get
        {
            if (serialized)
                return writer.ToReadOnlySequence();
            writer.Clear();
            writer.WriteInt32(Constructor, true);
            writer.WriteTLString(_langPack);
            serialized = true;
            return writer.ToReadOnlySequence();
        }
    }

    private string _langPack;
    public string LangPack
    {
        get => _langPack;
        set
        {
            serialized = false;
            _langPack = value;
        }
    }

    public async Task<ITLObject> ExecuteAsync(TLExecutionContext ctx)
    {
        RpcResult result = factory.Resolve<RpcResult>();
        result.ReqMsgId = ctx.MessageId;
        var langs = factory.Resolve<Vector<LangPackLanguageImpl>>();
        var languages = await _langPackService.GetLanguagesAsync(_langPack);
        foreach (var language in languages)
        {
            var lang = factory.Resolve<LangPackLanguageImpl>();
            lang.Beta = language.Beta;
            lang.Official = language.Official;
            lang.Rtl = language.Rtl;
            lang.Name = language.Name;
            lang.LangCode = language.LangCode;
            lang.NativeName = language.NativeName;
            lang.PluralCode = language.PluralCode;
            lang.StringsCount = language.StringsCount;
            lang.TranslationsUrl = language.TranslationsUrl;
            lang.BaseLangCode = language.BaseLangCode;
        }

        result.Result = langs;
        return result;
    }

    public void Parse(ref SequenceReader buff)
    {
        serialized = false;
        _langPack = buff.ReadTLString();
    }

    public void WriteTo(Span<byte> buff)
    {
        TLBytes.CopyTo(buff);
    }
}
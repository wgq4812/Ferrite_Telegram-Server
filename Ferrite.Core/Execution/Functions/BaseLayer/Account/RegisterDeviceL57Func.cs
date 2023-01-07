// 
// Project Ferrite is an Implementation of the Telegram Server API
// Copyright 2022 Aykut Alparslan KOC <aykutalparslan@msn.com>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
// 

using Ferrite.Services;
using Ferrite.TL;
using Ferrite.TL.slim;

namespace Ferrite.Core.Execution.Functions.BaseLayer.Account;

public class RegisterDeviceL57Func: ITLFunction
{
    private readonly IAccountService _account;

    public RegisterDeviceL57Func(IAccountService account)
    {
        _account = account;
    }
    public async ValueTask<TLBytes?> Process(TLBytes q, TLExecutionContext ctx)
    {
        using var register = await _account.RegisterDeviceL57(ctx.CurrentAuthKeyId, q);
        var rpcResult = RpcResultGenerator.Generate(register, ctx.MessageId);
        return rpcResult;
    }
}
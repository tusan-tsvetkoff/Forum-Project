using Forum.Server.Common.Interfaces;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;

namespace Forum.Server.Data;

public class TokenStorageService : ITokenStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public TokenStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task StoreToken(string token)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", token);
    }

    public async Task<string> RetrieveToken()
    {
        return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
    }

    public async Task RemoveToken()
    {
        await _jsRuntime.InvokeAsync<string>("localStorage.removeItem", "authToken");
    }

    public async Task<bool> CheckTokenExpiry()
    {
        var token = await RetrieveToken();

        if (token is null)
        {
            return true;
        }

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

        return jwt.ValidTo < DateTime.UtcNow;
    }
}

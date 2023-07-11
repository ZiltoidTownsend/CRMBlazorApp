using Application.Requests.Identity;
using Application.Responses.Identity;
using CRMBlazorApp.Shared.Wrapper;

namespace Application.Interfaces.Services.Identity;
public interface ITokenService
{
    Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

    Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
}

using Application.Requests.Identity;
using CRMBlazorApp.Shared.Wrapper;

namespace Application.Interfaces.Services.Account;
public interface IAccountService
{
    Task<IResult> UpdateProfileAsync(UpdateProfileRequest model, string userId);

    Task<IResult> ChangePasswordAsync(ChangePasswordRequest model, string userId);

    Task<IResult<string>> GetProfilePictureAsync(string userId);

    // to do change picture request?
}

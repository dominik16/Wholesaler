using Wholesaler.DataTransferObject;

namespace Wholesaler.Services
{
    public interface IAuthService
    {
        Task RegisterUser(CreateUserDto dto);
        Task<string> GenerateJwt(LoginUserDto userDto);
        Task<bool> CheckUserExists(LoginUserDto userDto);
    }
}

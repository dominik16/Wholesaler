using Wholesaler.DataTransferObject;

namespace Wholesaler.Services
{
    public interface IManageService
    {
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUser(int id);
        Task DeleteUser(int id);
        Task ChangeRole(int userId, int roleId);
        Task<bool> CheckExistsUser(int id);
        Task<bool> CheckRoleExists(int roleId);
    }
}

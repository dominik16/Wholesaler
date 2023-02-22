using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using Wholesaler.Data;
using Wholesaler.DataTransferObject;
using Wholesaler.Models;

namespace Wholesaler.Services
{
    public class ManageService : IManageService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ManageService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task ChangeRole(int userId, int roleId)
        {
            var user = await _context.Users.FirstAsync(u => u.Id == userId);
            user.RoleId = roleId;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckExistsUser(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<bool> CheckRoleExists(int roleId)
        {
            return await _context.Roles.AnyAsync(r => r.Id == roleId);
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FirstAsync(u => u.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return usersDto;
        }

        public async Task<UserDto> GetUser(int id)
        {
            var user = await _context.Users.FirstAsync(u => u.Id == id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wholesaler.Data;
using Wholesaler.DataTransferObject;
using Wholesaler.Exceptions;
using Wholesaler.Models;

namespace Wholesaler.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthSettings _authSetings;

        public AuthService(DataContext context, IMapper mapper, IPasswordHasher<User> passwordHasher, AuthSettings authSetings)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authSetings = authSetings;
        }

        public async Task<bool> CheckUserExists(LoginUserDto userDto)
        {
            return await _context.Users.AnyAsync(u => u.Email == userDto.Email);
        }

        public async Task<string> GenerateJwt(LoginUserDto userDto)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == userDto.Email);

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userDto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSetings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authSetings.JwtExpireDays);

            var token = new JwtSecurityToken(_authSetings.JwtIssuer, _authSetings.JwtIssuer, claims, expires: expires, signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public async Task RegisterUser(CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            var hashedPassword = _passwordHasher.HashPassword(user, userDto.Password);
            user.PasswordHash = hashedPassword;

            user.RoleId = 1;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}

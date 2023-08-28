using Dotnet.Web.Interfaces;
using Dotnet.Web.Data;
using Dotnet.Web.Dto;
using Dotnet.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Dotnet.Web.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserService(
            AppDbContext context, 
            UserManager<User> userManager, 
            IHttpContextAccessor httpContextAccessor) 
        {
            this.context = context;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }
        public int GetUserIdFromClaims()
        {
            var uridClaim = httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(cl=>cl.Type == ClaimTypes.NameIdentifier);
            return (uridClaim != null ? int.Parse(uridClaim.Value) : -1);

        }
        public async Task<UserDto> GetUser() {
            var userId = GetUserIdFromClaims();
            var user = await context.Users.FirstOrDefaultAsync(user => user.Id == userId);
            return new UserDto()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public async Task<LoginResponseDto> Login(LoginDto dto) {
            User? user = await context.Users.FirstOrDefaultAsync(user => user.Email!.Equals(dto.Email));
            if (user == null) { throw new UnauthorizedAccessException("User is not authorized"); }

            var roleId = context.UserRoles.Where(role => role.UserId == user.Id).First().RoleId;
            var userRole = context.Roles.Where(role => role.Id == roleId).First();

            var claims = new List<Claim> {
            new Claim(ClaimTypes.Email, dto.Email!),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Role, userRole.Name!),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            var loginDto = new LoginResponseDto()
            {
                UserName = user.UserName!,
                Token = new JwtSecurityTokenHandler().WriteToken(jwt)
            };

            return loginDto;
        }


        public async Task<bool> Register(RegisterDto registerDto) {

            if (registerDto.Email == null || registerDto.UserName == null)
            {
                throw new ArgumentNullException();
            }

            var pwHasher = new PasswordHasher<User>();
            var curMaxId = context.Users.Select(u => u.Id).DefaultIfEmpty().Max();
            var newUser = new User()
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                Id = curMaxId + 1,

            };

            newUser.PasswordHash = pwHasher.HashPassword(newUser, registerDto.Password);
            newUser.SecurityStamp = Guid.NewGuid().ToString();
            context.Users.Add(newUser);
            context.SaveChanges();

            var userChecked = context.Users.FirstOrDefault(u => u.Id == newUser.Id);

            if (userChecked == null) { throw new BadHttpRequestException("Check your data"); }

            var role = context.Roles.FirstOrDefault(r => r.Name == "User");
            var res = await userManager.AddToRoleAsync(userChecked, "User");
            context.SaveChanges();

            if (res.Succeeded) { return true; } else { return false; }

        }


        public async Task<UserDto?> GetUserByEmail(string email)
        {
            if (email == null) { throw new ArgumentNullException(); }

            if (httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Role) != "Admin") { 
                throw new UnauthorizedAccessException("Access denied"); 
            }

            var user = context.Users.FirstOrDefault(ur => ur.Email == email);
            if(user == null) { return null; }

            return new UserDto() { Email = email, UserName = user.UserName!, UserId = user.Id };

        }
    }
}

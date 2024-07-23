using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AttendanceApi.Dto.Response;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AttendanceApi.Service.Impl
{
    public class LoginService : ILoginService
    {
        private readonly AttendanceContext _context;
        private readonly IConfiguration _configuration;

        public LoginService(AttendanceContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public AuthResponse Authenticate(string email, string password)
        {
            var student = _context.Students
                .Include(s => s.StudentRoles)
                    .ThenInclude(sr => sr.Role)
                .SingleOrDefault(s => s.Email == email && s.Password == password);

            if (student == null)
                return null;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, student.Id.ToString()),
                new Claim(ClaimTypes.Name, student.Name)
            };
            foreach (var studentRole in student.StudentRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, studentRole.Role.Name));
                var permissions = _context.RolePermissions
                    .Where(rp => rp.RoleId == studentRole.RoleId)
                    .Select(rp => rp.Permission.Name)
                    .ToList();

                foreach (var permission in permissions)
                {
                    claims.Add(new Claim("Permission", permission));
                }
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"])),
                signingCredentials: creds);

            var response = new AuthResponse
            {
                Successful = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
                
            };
            return response;
        }

        public IEnumerable<PermissionResponse> GetPermissions(int userId)
        {
            throw new NotImplementedException();
        }
    }
}

using AttendanceApi.Dto.Response;
using AttendanceApi.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Service.Impl
{
    public class RoleService : IRoleService
    {
        private readonly AttendanceContext _context;
        private readonly IMapper _mapper;

        public RoleService(AttendanceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<RoleResponse> GetListRole()
        {
            var roles = _context.Roles
                .Include(r => r.StudentRoles)
                    .ThenInclude(sr => sr.Student)
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .ToList();
            var roleResponses = _mapper.Map<List<RoleResponse>>(roles);
            return roleResponses;
        }
    }
}

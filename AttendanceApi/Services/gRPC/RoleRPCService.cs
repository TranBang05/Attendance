using Grpc.Core;
using AttendanceApi.Protos;
using AttendanceApi.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Services.gRPC
{
    public class RoleRPCService : RoleRPC.RoleRPCBase
    {
        private readonly AttendanceContext _context;

        public RoleRPCService(AttendanceContext context)
        {
            _context = context;
        }

        public override async Task<RoleResponse> GetRole(RoleRequest request, ServerCallContext context)
        {
            var role = await _context.Roles
                .Include(r => r.StudentRoles)
                    .ThenInclude(sr => sr.Student)
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(r => r.Id == request.Id);

            if (role == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Role not found"));
            }

            var roleResponse = new RoleResponse
            {
                Id = role.Id,
                RoleName = role.Name,
                StudentRoles = { role.StudentRoles.Select(sr => new StudentRoles { StudentName = sr.Student.Name }) },
                RolePermissions = { role.RolePermissions.Select(rp => new RolePermissions { PermissionName = rp.Permission.Name }) }
            };

            return roleResponse;
        }

    }
}

using AttendanceApi.Models;

namespace AttendanceApi.Dto.Response
{
    public class RoleResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<StudentRoleResponse>? StudentRoles { get; set; }
        public ICollection<RolePermissionResponse>? RolePermissions { get; set; }
    }
}

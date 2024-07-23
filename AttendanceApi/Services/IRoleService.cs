using AttendanceApi.Dto.Response;

namespace AttendanceApi.Service
{
    public interface IRoleService
    {
        public List<RoleResponse> GetListRole();
    }
}

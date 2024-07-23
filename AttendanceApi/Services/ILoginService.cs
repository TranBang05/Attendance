using AttendanceApi.Dto.Response;

namespace AttendanceApi.Service
{
    public interface ILoginService
    {
        AuthResponse Authenticate(string email, string password);
        IEnumerable<PermissionResponse> GetPermissions(int userId);
    }
}

using AttendanceApi.Dto.Response;

namespace AttendanceApi.Service
{
    public interface ITeacherService
    {
        public List<TeacherResponse> Teachers();
    }
}

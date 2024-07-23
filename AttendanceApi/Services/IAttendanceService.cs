using AttendanceApi.Dto.Request;

namespace AttendanceApi.Service
{
    public interface IAttendanceService
    {
        public bool UpdateAttendance(AttendanceRequest attendanceRequest);

        public string AddAttendance(AttendanceRequest attendanceRequest);
    }
}

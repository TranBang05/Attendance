using AttendanceApi.Models;

namespace AttendanceApi.Dto.Request
{
    public class StudentAttendanceRequest
    {
        public int StudentId {  get; set; }
        public AttendanceStatus Status { get; set; }
    }
}

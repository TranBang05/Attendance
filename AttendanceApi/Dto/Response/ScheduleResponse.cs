using AttendanceApi.Models;

namespace AttendanceApi.Dto.Response
{
    public class ScheduleResponse
    {
        public int Id { get; set; }
        public string? Slot { get; set; }
        public DateTime? Date { get; set; }
        public string? CourseName { get; set; }
        public string? CourseCode { get; set; }
        public string? TeacherName { get; set; }
    }
}

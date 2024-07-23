using AttendanceApi.Models;

namespace AttendanceApi.Dto.Response
{
    public class TeacherResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<ScheduleResponse>? Schedules { get; set; }

    }
}

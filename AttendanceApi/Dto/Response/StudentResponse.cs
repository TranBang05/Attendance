namespace AttendanceApi.Dto.Response
{
    public class StudentResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Mssv { get; set; }
        public List<CourseResponse>? Courses { get; set; }
        public List<ScheduleResponse>? Schedules { get; set; }
    }
}

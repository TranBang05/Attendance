namespace AttendanceApi.Dto.Response
{
    public class CourseResponse
    {
        public int Id { get; set; }
        public string? Code { get; set; }
       // public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? SubjectName { get; set; }
    }
}

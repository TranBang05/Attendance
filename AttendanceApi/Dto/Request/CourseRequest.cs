namespace AttendanceApi.Dto.Request
{
    public class CourseRequest
    {
        public int Id { get; set; } 
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int? SubjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int>? StudentIds { get; set; }
        public string? TimeSlot { get; set; }
        public int TeacherId { get; set; }
    }
}

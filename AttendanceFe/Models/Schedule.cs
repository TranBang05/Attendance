namespace AttendanceFe.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string? Slot { get; set; }
        public DateTime? Date { get; set; }
        public string? CourseName { get; set; }
        public string? CourseCode { get; set; }
        public string? TeacherName { get; set; }
    }
}

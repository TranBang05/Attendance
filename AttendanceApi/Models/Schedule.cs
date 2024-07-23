namespace AttendanceApi.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string? Slot { get; set; }
        public DateTime? Date { get; set; }
        public int? CourseId { get; set; }
        public int? TeacherId { get; set; }

        public  Course? Course { get; set; }
        public  Teacher? Teacher { get; set; }
        public  ICollection<StudentSchedule>? StudentSchedules { get; set; }
    }
}

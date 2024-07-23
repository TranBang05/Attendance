namespace AttendanceApi.Models
{
    public class StudentSchedule
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ScheduleId { get; set; }
        public AttendanceStatus Status { get; set; }

        public Schedule? Schedule { get; set; }
        public Student? Student { get; set; }
    }
}

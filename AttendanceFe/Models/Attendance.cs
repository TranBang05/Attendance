namespace AttendanceFe.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ScheduleId { get; set; }
        public AttendanceStatus Status { get; set; }

        // Thông tin chi tiết về lịch học
        public string? CourseName { get; set; }
        public string? TeacherName { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string? StudentName { get; set; }
    }
}

namespace AttendanceFe.Models
{
    public class StudentAttendanceRequest
    {
        public int StudentId { get; set; }
        public AttendanceStatus Status { get; set; }
    }
}

namespace AttendanceApi.Dto.Request
{
    public class AttendanceRequest
    {
        public int ScheduleId {  get; set; }
        public List<StudentAttendanceRequest>? Students { get; set; }
    }
}

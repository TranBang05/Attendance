namespace AttendanceApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Mssv { get; set; }

        public ICollection<StudentCourse>? StudentCourses { get; set; }
        public ICollection<StudentSchedule>? StudentSchedules { get; set; }
        public ICollection<StudentRole>? StudentRoles { get; set; }
    }
}

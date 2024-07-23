namespace AttendanceApi.Models
{
    public class StudentRole
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }
}

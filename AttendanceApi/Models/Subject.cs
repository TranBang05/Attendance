namespace AttendanceApi.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int NumberSlot { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
    
}

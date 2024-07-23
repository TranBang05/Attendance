using System.ComponentModel.DataAnnotations;

namespace AttendanceApi.Dto.Request
{
    public class SubjectRequest
    {
        public int Id { get; set; }
       
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int? NumberSlot { get; set; }
    }
}

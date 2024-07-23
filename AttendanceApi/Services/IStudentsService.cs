using AttendanceApi.Dto.Response;

namespace AttendanceApi.Service
{
    public interface IStudentsService
    {
        public List<StudentResponse> GetAll();
         public StudentResponse GetbyId(int id);
    }
}

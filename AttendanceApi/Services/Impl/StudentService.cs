using AttendanceApi.Dto.Response;
using AttendanceApi.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Service.Impl
{
    public class StudentService :IStudentsService
    {
        private readonly AttendanceContext _context;
        private readonly IMapper _mapper;

        public StudentService(AttendanceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<StudentResponse> GetAll()
        {
            var students = _context.Students
              .Include(s => s.StudentCourses)
              .ThenInclude(sc => sc.Course)
              .ThenInclude(c => c.Subject)
              .Include(s => s.StudentSchedules)
              .ThenInclude(ss => ss.Schedule)
              .ThenInclude(sch => sch.Teacher)
              .ToList();

            var result = _mapper.Map<List<StudentResponse>>(students);
            return result;
        }

        public StudentResponse GetbyId(int id)
        {
            var student = _context.Students
                .Include(s => s.StudentCourses)
                    .ThenInclude(sc => sc.Course)
                        .ThenInclude(c => c.Subject)
                .Include(s => s.StudentSchedules)
                    .ThenInclude(ss => ss.Schedule)
                        .ThenInclude(sch => sch.Teacher)
                .FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                throw new KeyNotFoundException($"Student with ID {id} not found.");
            }

            var studentResponse = _mapper.Map<StudentResponse>(student);
            return studentResponse;
        }

    }
}

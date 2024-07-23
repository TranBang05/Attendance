using AttendanceApi.Dto.Response;
using AttendanceApi.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Service.Impl
{
    public class TeacherSerivce : ITeacherService
    {
        private readonly AttendanceContext _context;
        private readonly IMapper _mapper;

        public TeacherSerivce(AttendanceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<TeacherResponse> Teachers()
        {
            var teachers = _context.Teachers
            .Include(t => t.Schedules)
            .ThenInclude(s => s.Course)
            .ToList();

            var teacherResponses = _mapper.Map<List<TeacherResponse>>(teachers);

            return teacherResponses;
        }
    }
}

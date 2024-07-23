using AttendanceApi.Dto.Response;
using AttendanceApi.Service;
using AttendanceApi.Service.Impl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace AttendanceApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentsService _student;

        public StudentController(IStudentsService student)
        {
            _student = student;
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<List<StudentResponse>> GetAllStudentsWithDetails()
        {
            var students = _student.GetAll();
            return Ok(students);
        }

        [HttpGet("{id}/schedules")]
        [EnableQuery]
        public IActionResult GetStudentSchedules(int id)
        {
            var student = _student.GetbyId(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student.Schedules);
        }

    }
}

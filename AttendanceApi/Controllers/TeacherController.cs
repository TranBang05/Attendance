using AttendanceApi.Dto.Response;
using AttendanceApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : Controller
    {

        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }


        [HttpGet]
        public ActionResult<List<TeacherResponse>> GetTeachers()
        {
            var teachers = _teacherService.Teachers();
            return Ok(teachers);
        }
    }
}

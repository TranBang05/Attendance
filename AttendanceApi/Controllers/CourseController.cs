using AttendanceApi.Dto.Request;
using AttendanceApi.Dto.Response;
using AttendanceApi.Models;
using AttendanceApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly AttendanceContext _context;

        public CourseController(ICourseService courseService, AttendanceContext context)
        {
            _courseService = courseService;
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateCourse([FromBody] CourseRequest courseRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _courseService.CreateCourse(courseRequest);
                return Ok("Khóa học đã được tạo thành công.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi khi tạo khóa học.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id, [FromBody] CourseRequest courseRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _courseService.UpdateCourse(id, courseRequest);
                return Ok("Khóa học đã được cập nhật thành công.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi khi cập nhật khóa học.");
            }
        }



        [HttpGet]
        public ActionResult<List<CourseResponse>> GetCourse()
        { 

            try
            {
                var subjects = _courseService.getListCourse();
                return Ok(subjects);
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("id")]
        public IActionResult GetCourse(int id)
        {

            try
            {
                var subjects = _courseService.getCourse(id);
                return Ok(subjects);
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("teachers")]
        public IActionResult GetTeachers()
        {
            var teachers = _context.Teachers.ToList();
            return Ok(teachers);
        }

        [HttpGet("subjects")]
        public IActionResult getSubjects()
        {
            var s = _context.Subjects.ToList();
            return Ok(s);
        }

        [HttpGet("students")]
        public IActionResult GetStudents()
        {
            var students = _context.Students.ToList();
            return Ok(students);
        }

        [HttpGet("studentsbyschedule/{scheduleId}")]
        public ActionResult<List<StudentScheduleResponse>> GetStudentsByScheduleId(int scheduleId)
        {
            try
            {
                var students = _courseService.GetStudentsByScheduleId(scheduleId);
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}

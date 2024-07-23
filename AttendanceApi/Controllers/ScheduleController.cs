using AttendanceApi.Dto.Response;
using AttendanceApi.Service;
using AttendanceApi.Service.Impl;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : Controller
    {
        private readonly ICourseService _courseService;

        public ScheduleController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public ActionResult<List<ScheduleResponse>> GetSchedule()
        {


            try
            {
                var subjects = _courseService.getListSchedule();
                return Ok(subjects);
            }
            
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public ActionResult<List<ScheduleResponse>> GetSchedule(int Id)
        {
            try
            {
                var subjectResponse = _courseService.getSchedule(Id);

                if (subjectResponse == null)
                {
                    return NotFound(); 
                }

                return Ok(subjectResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("/schedules/{studentId}")]
        public ActionResult<List<ScheduleResponse>> GetSchedulesByStudentId(int studentId)
        {
            var schedules = _courseService.getSchedule(studentId);
            return Ok(schedules);
        }

    }
}

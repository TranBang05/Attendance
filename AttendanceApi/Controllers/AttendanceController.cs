using AttendanceApi.Dto.Request;
using AttendanceApi.Service;
using AttendanceApi.Service.Impl;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _Service;

        public AttendanceController(IAttendanceService Service)
        {
           _Service = Service;
        }

        [HttpPut("update")]
        public IActionResult UpdateAttendance([FromBody] AttendanceRequest attendanceRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = _Service.UpdateAttendance(attendanceRequest);
                if (result)
                {
                    return Ok("Update successful");
                }
                else
                {
                    return BadRequest("Update failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost("add")]
        public IActionResult AddAttendance([FromBody] AttendanceRequest attendanceRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = _Service.AddAttendance(attendanceRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

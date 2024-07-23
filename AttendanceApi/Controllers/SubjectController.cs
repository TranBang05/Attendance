using AttendanceApi.Dto.Request;
using AttendanceApi.Dto.Response;
using AttendanceApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        //[Authorize(Policy = "Student")]
        
        //[Authorize(Policy = "View")]
        public ActionResult<List<SubjectResponse>> GetSubjects()
        {
           

            try
            {
                var subjects = _subjectService.listSubject();
                return Ok(subjects);
            }
            catch (UnauthorizedAccessException)
            {
                var messageResponse = new MessageResponse(403, "Bạn không được xem tài nguyên này");
                return StatusCode(messageResponse.StatusCode, messageResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{subjectId}")]
        public IActionResult GetSubject(int subjectId)
        {
            try
            {
                var subjectResponse = _subjectService.GetSubject(subjectId);

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

        [HttpPost]
        //[Authorize(Policy = "Admin")]
        public IActionResult CreateSubject([FromBody] SubjectRequest subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _subjectService.addSubject(subject);
            return Ok();
        }

        [HttpPost("add")]
        //[Authorize(Policy = "Admin")]
        //[Authorize(Policy = "Create")] 
        public IActionResult AddSubject([FromBody] SubjectRequest subjectRequest)
        {
            try
            {
                _subjectService.addSubject(subjectRequest);
                return Ok("Subject added successfully");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(); // Trả về lỗi 403 Forbidden nếu không có quyền hạn
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateSubject([FromBody] SubjectRequest subjectRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _subjectService.UpdateSubject(subjectRequest);

            if (result == "Mon hoc không tồn tại")
            {
                return NotFound(result);
            }

            return Ok(result);
        }


        [HttpDelete("{id}")]
        //[Authorize(Policy = "Admin")]
        public IActionResult DeleteSubject(int id)
        {
            
            var result = _subjectService.DeleteSubject(id);
            return Ok(result);
        }

    }
}

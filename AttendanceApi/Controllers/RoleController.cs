using AttendanceApi.Dto.Response;
using AttendanceApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService Service)
        {
            _roleService = Service;
        }
        [HttpGet]
        public ActionResult<List<RoleResponse>> GetRole()
        {
            try
            {
                var s = _roleService.GetListRole();
                return Ok(s);
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
    }
}

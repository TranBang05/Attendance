using AttendanceApi.Dto.Request;
using AttendanceApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {

        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] AuthRequest model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest("Email and password are required.");
            }

            var authResponse = _loginService.Authenticate(model.Email, model.Password);

            if (authResponse == null || !authResponse.Successful)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(authResponse);
        }

    }
}

using AttendanceFe.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AttendanceFe.Controllers
{
    public class StudentController : Controller
    {

        private readonly ILogger<SubjectController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public StudentController(ILogger<SubjectController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public async Task<IActionResult> StudentDetail()
        {
            var url = "http://localhost:5095/api/Student";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<Student>>(data);
                return View(users);
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to retrieve user list.";
                return View();
            }
        }
        

    }
}

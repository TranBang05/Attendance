using System.Text;
using AttendanceFe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace AttendanceFe.Controllers
{
    public class CourseController : Controller
    {
        private readonly ILogger<SubjectController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
       
        public CourseController(ILogger<SubjectController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        private async Task<List<SelectListItem>> GetSubjectsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5095/api/Subject");
                response.EnsureSuccessStatusCode();

                var data = await response.Content.ReadAsStringAsync();
                var subjects = JsonConvert.DeserializeObject<List<Subject>>(data);

                var subjectList = subjects?.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList() ?? new List<SelectListItem>();

                return subjectList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving subjects: {ex.Message}");
                return new List<SelectListItem>();
            }
        }

        private async Task<List<SelectListItem>> GetTeachersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5095/api/Teacher");
                response.EnsureSuccessStatusCode();

                var data = await response.Content.ReadAsStringAsync();
                var subjects = JsonConvert.DeserializeObject<List<Teacher>>(data);
                var subjectList = subjects.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList();

                return subjectList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving subjects: {ex.Message}");
                throw; 
            }
        }

        private async Task<List<SelectListItem>> GetStudentsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5095/api/Student");
                response.EnsureSuccessStatusCode();

                var data = await response.Content.ReadAsStringAsync();
                var students = JsonConvert.DeserializeObject<List<Student>>(data);
                var studentList = students.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList();

                return studentList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving students: {ex.Message}");
                throw;
            }
        }



        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.Subjects = await GetSubjectsAsync();
                ViewBag.Teachers = await GetTeachersAsync();
                ViewBag.Students = await GetStudentsAsync();
                var viewModel = new CourseRequest();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading Create page: {ex.Message}", ex);
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseRequest viewModel)
        {
            var url = "http://localhost:5095/api/Course";
            var jsonContent = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, jsonContent);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("CourseDetail");
            else
            {
                ViewBag.ErrorMessage = "Failed to create subject.";
                return View(viewModel);
            }
        }

        public async Task<IActionResult> CourseDetail()
        {
            var url = $"http://localhost:5095/api/Course";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<Course>>(data);
                return View(users);
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to retrieve user list.";
                return View();
            }
        }


        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"http://localhost:5095/api/Course/id?id={id}");
                response.EnsureSuccessStatusCode();


                var data = await response.Content.ReadAsStringAsync();
                var course = JsonConvert.DeserializeObject<CourseRequest>(data);

                ViewBag.Subjects = await GetSubjectsAsync();
                ViewBag.Teachers = await GetTeachersAsync();
                ViewBag.Students = await GetStudentsAsync();

                return View(course);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading Edit page: {ex.Message}");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, CourseRequest viewModel)
        {
            var url = $"http://localhost:5095/api/Course/{id}";
            var jsonContent = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, jsonContent);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("CourseDetail");
            else
            {
                ViewBag.ErrorMessage = "Failed to update course.";
                ViewBag.Subjects = await GetSubjectsAsync();
                ViewBag.Teachers = await GetTeachersAsync();
                ViewBag.Students = await GetStudentsAsync();
                return View(viewModel);
            }
        }

    }
}

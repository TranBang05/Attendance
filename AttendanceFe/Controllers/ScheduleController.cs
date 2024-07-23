using AttendanceFe.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace AttendanceFe.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ILogger<SubjectController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ScheduleController(ILogger<SubjectController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public async Task<IActionResult> Attendance(int id)
        {
            var url = $"http://localhost:5095/api/Course/studentsbyschedule/{id}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var subjects = JsonConvert.DeserializeObject<List<Attendance>>(data);
                return View(subjects);
            }
            else
            {
                ViewBag.ErrorMessage = "Không thể lấy thông tin môn học để chỉnh sửa.";
                return View(new List<Attendance>());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Attendance(List<Attendance> subjects)
        {
            try
            {
                var attendanceRequest = new AttendanceRequest
                {
                    ScheduleId = subjects.First().ScheduleId,
                    Students = subjects.Select(s => new StudentAttendanceRequest
                    {
                        StudentId = s.StudentId,
                        Status = s.Status
                    }).ToList()
                };

                var apiUrl = "http://localhost:5095/api/Attendance/update"; // URL của API
                var jsonContent = new StringContent(JsonConvert.SerializeObject(attendanceRequest), Encoding.UTF8, "application/json");

                // Ghi log JSON request
                _logger.LogInformation("JSON request: " + await jsonContent.ReadAsStringAsync());

                // Gửi request PUT đến API
                var response = await _httpClient.PutAsync(apiUrl, jsonContent);

                // Ghi log trạng thái HTTP response
                _logger.LogInformation("HTTP response status: " + response.StatusCode);

                // Ghi log nội dung response
                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Response content: " + responseContent);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Cập nhật điểm danh thành công.";
                    return RedirectToAction("Attendance", new { id = subjects.First().ScheduleId });
                }
                else
                {
                    ViewBag.ErrorMessage = "Không thể cập nhật điểm danh: " + responseContent;
                    return View(subjects);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gọi API cập nhật điểm danh");
                ViewBag.ErrorMessage = $"Lỗi: {ex.Message}";
                return View(subjects);
            }
        }


    }
}

using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using AttendanceFe.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AttendanceFe.Protos;
using Grpc.Net.Client;
namespace AttendanceFe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly SubjectRPC.SubjectRPCClient _subjectRPCClient;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            var channel = GrpcChannel.ForAddress("http://localhost:5095");
            _subjectRPCClient = new SubjectRPC.SubjectRPCClient(channel);

        }


        public async Task<IActionResult> SubjectDetail()
        {
            var response = await _subjectRPCClient.GetAllSubjectsAsync(new Empty());

            var subjectListViewModel = response.Subjects.Select(subject => new Subject
            {
                Id = subject.Id,
                Code = subject.Code,
                Name = subject.Name,
                NumberSlot = subject.NumberSlot
            }).ToList();

            return View(subjectListViewModel);

        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var apiUrl = "http://localhost:5095/api/Login"; // URL của API Login
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    // Gửi request POST đến API
                    var response = await _httpClient.PostAsync(apiUrl, jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var authResponse = JsonConvert.DeserializeObject<AuthResponse>(responseData);

                        // Lưu token vào Session
                        HttpContext.Session.SetString("JWToken", authResponse.Token);

                        // Redirect đến trang chính (hoặc trang dashboard)
                        return RedirectToAction("trangchu", "Home");
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        ViewBag.ErrorMessage = "Invalid email or password.";
                        return View();
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to authenticate.";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while calling API Login");
                    ViewBag.ErrorMessage = $"Error: {ex.Message}";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please provide valid credentials.";
                return View();
            }
        }


        public IActionResult trangchu()
        {
            return View();
        }
       
        /*
        public async Task<IActionResult> SubjectDetail()
        {
            try
            {
                var url = "http://localhost:5095/api/Subject";
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var token = HttpContext.Session.GetString("JWToken");
                if (token != null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var subjects = JsonConvert.DeserializeObject<List<Subject>>(data);
                    return View(subjects);
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    ViewBag.ErrorMessage = "Bạn không có quyền truy cập vào tài nguyên này.";
                    return View(new List<Subject>());
                }
                else
                {
                    ViewBag.ErrorMessage = "Đã xảy ra lỗi khi truy cập tài nguyên.";
                    return View(new List<Subject>());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gọi API");
                ViewBag.ErrorMessage = $"Lỗi: {ex.Message}";
                return View(new List<Subject>());
            }
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
        */



        public async Task<IActionResult> Schedule()
        {
            var url = "http://localhost:5095/api/Schedule";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<Schedule>>(data);
                return View(users);
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to retrieve user list.";
                return View();
            }
        }


        public async Task<IActionResult> StudentSchedule(int id)
        {
            var url = $"http://localhost:5095/schedules/{id}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<Schedule>>(data);
                return View(users);
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to retrieve user list.";
                return View();
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


       

        public async Task<IActionResult> Delete(int id)
        {
            var url = $"http://localhost:5095/api/Subject/{id}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var subject = JsonConvert.DeserializeObject<Subject>(data);
                return View(subject);
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to retrieve subject.";
                return RedirectToAction("SubjectDetail");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System.Net;
using System.Net.Http.Headers;
using System.Text;
using AttendanceFe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using AttendanceFe.Protos;
using Grpc.Net.Client;
using AttendanceFe.Models.Request;
using AttendanceFe.Service;

namespace AttendanceFe.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ILogger<SubjectController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly SubjectRPC.SubjectRPCClient _subjectRPCClient;

        private readonly SubjectService _service;
        public SubjectController(ILogger<SubjectController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration, SubjectService service, SubjectRPC.SubjectRPCClient subjectRPCClient)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
           
            _service = service;
            _subjectRPCClient=subjectRPCClient;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubjectRequestAdd subjectRequest)
        {
            var request = new SubjectRequestAdd
            {
                Code = subjectRequest.Code,
                Name = subjectRequest.Name,
                NumberSlot = subjectRequest.NumberSlot
            };

            var response = await _subjectRPCClient.CreateSubjectAsync(request);

            return RedirectToAction("SubjectDetail", "Home");
        }


        public async Task<IActionResult> Update(int id)
        {
            var request = new Protos.SubjectRequest { Id = id };
            var response = await _subjectRPCClient.GetSubjectsAsync(request);

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SubjectRequestUpdate subjectRequest)
        {
            var request = new SubjectRequestUpdate
            {
                Id = subjectRequest.Id,
                Code = subjectRequest.Code,
                Name = subjectRequest.Name,
                NumberSlot = subjectRequest.NumberSlot
            };

            var response = await _subjectRPCClient.UpdateSubjectAsync(request);


            return RedirectToAction("SubjectDetail", "Home");
        }





        public async Task<IActionResult> Delete(int id)
        {
            var request = new Protos.SubjectRequest { Id = id };
            var response = await _subjectRPCClient.GetSubjectsAsync(request);

            return View(response);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = new Protos.SubjectRequest { Id = id };
            await _subjectRPCClient.DeleteSubjectAsync(request);

            return RedirectToAction("SubjectDetail", "Home");
        }
        /*
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(sRequest request)
        {
            await _service.CreateSubjetcAsync(request);
            return RedirectToAction("SubjectDetail", "Home");
        }

        */

        /*
        [HttpPost]
        public async Task<IActionResult> Create(sRequest subjectRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(subjectRequest);
            }

            try
            {
                var gpcRequest = new SubjectRequestAdd
                {
                    Code = subjectRequest.Code,
                    Name = subjectRequest.Name,
                    NumberSlot = subjectRequest.NumberSlot
                };

                var response = await _subjectRPCClient.CreateSubjectAsync(gpcRequest);

                if (response != null && response.Sucess)
                {
                    TempData["SuccessMessage"] = "Môn học đã được tạo thành công.";
                    return RedirectToAction("SubjectDetail");
                }
                else
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi tạo môn học.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi tạo môn học: " + ex.Message);
            }

            return View(subjectRequest);
        }

        */

        public async Task<IActionResult> SubjectDetail()
        {
            try
            {
                var url = "http://localhost:5095/api/Subject";
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                // Lấy token từ Session để gửi cùng yêu cầu
                var token = HttpContext.Session.GetString("JWToken");
                if (token != null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
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

        /*
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subject subject)
        {
            var url = "http://localhost:5095/api/Subject";
            var jsonContent = new StringContent(JsonConvert.SerializeObject(subject), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, jsonContent);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("SubjectDetail");
            else
            {
                ViewBag.ErrorMessage = "Failed to create subject.";
                return View(subject);
            }
        }
        */

        /*
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var url = $"http://localhost:5095/api/Subject/{id}";
                var response = await _httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Json(new { success = true, message = result });
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = errorContent });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        public async Task<IActionResult> Update(int id)
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
                ViewBag.ErrorMessage = "Không thể lấy thông tin môn học để chỉnh sửa.";
                return View();
            }
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Subject subject)
        {
            var url = $"http://localhost:5095/api/Subject";
            var jsonContent = new StringContent(JsonConvert.SerializeObject(subject), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("SubjectDetail", "Home");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                ViewBag.ErrorMessage = "Không tìm thấy môn học.";
                return View(subject);
            }
            else
            {
                ViewBag.ErrorMessage = "Không thể cập nhật thông tin môn học.";
                return View(subject);
            }
        }
        */



    }
}

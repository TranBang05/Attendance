using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using AttendanceFe.Protos;
using AttendanceFe.Models;
using AttendanceFe.Models.Request;

namespace AttendanceFe.Controllers
{
    public class SubjectGPCController : Controller
    {
        private readonly SubjectRPC.SubjectRPCClient _subjectRPCClient;

        public SubjectGPCController()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5095");
            _subjectRPCClient = new SubjectRPC.SubjectRPCClient(channel);
        }

        public async Task<IActionResult> SubjectDetail()
        {
            var response = await _subjectRPCClient.GetAllSubjectsAsync(new Empty());

            var subjectListViewModel = response.Subjects.Select(subject => new SubjectResponse
            {
                Id = subject.Id,
                Code = subject.Code,
                Name = subject.Name,
                NumberSlot = subject.NumberSlot
            }).ToList();

            return View(subjectListViewModel);

        }

        public async Task<IActionResult> Create(sRequest subjectRequest)
        {
            try
            {
                var gpcRequest = new SubjectRequestAdd
                {
                    Code = subjectRequest.Code,
                    Name = subjectRequest.Name,
                    NumberSlot = subjectRequest.NumberSlot
                };

                var response = await _subjectRPCClient.CreateSubjectAsync(gpcRequest);
                Console.WriteLine("CreateSubjectAsync response: " + response.ToString());

                return RedirectToAction("/Home/SubjectDetail");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi tạo môn học: " + ex.Message);
                return View(subjectRequest);
            }
        }


    }
}

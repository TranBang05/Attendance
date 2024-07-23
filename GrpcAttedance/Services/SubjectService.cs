using Grpc.Core;
using GrpcAttendanceApi.Protos;
using System.Threading.Tasks;

namespace GrpcAttendance.Services
{
    public class SubjectService : SubjectRPC.SubjectRPCBase
    {
        // Ví dụ: một danh sách tạm thời của các subjects
        private readonly List<SubjectResponse> _subjects = new List<SubjectResponse>
        {
            new SubjectResponse { Id = 1, Code = "CS101", Name = "Computer Science", NumberSlot = 3 },
            new SubjectResponse { Id = 2, Code = "MTH101", Name = "Mathematics", NumberSlot = 4 },
        };

        public override Task<SubjectResponse> GetSubjects(SubjectRequest request, ServerCallContext context)
        {
            var subject = _subjects.FirstOrDefault(s => s.Id == request.Id);
            if (subject == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Subject not found"));
            }

            return Task.FromResult(subject);
        }
    }
}
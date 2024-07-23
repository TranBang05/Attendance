using Grpc.Core;
using AttendanceApi.Protos;
using AttendanceApi.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Services.gRPC
{
    public class SubjectGPCService : SubjectRPC.SubjectRPCBase
    {
        private readonly AttendanceContext _context;

        public SubjectGPCService(AttendanceContext context)
        {
            _context = context;
        }

        public override async Task<SubjectResponse> GetSubjects(SubjectRequest request,
                                                                 ServerCallContext context)
        {
            var subject = await _context.Subjects.FindAsync(request.Id);

            if (subject != null)
            {
                var response = new SubjectResponse
                {
                    Id = subject.Id,
                    Code = subject.Code,
                    Name = subject.Name,
                    NumberSlot = subject.NumberSlot,
                };

                return response;
            }
            else
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Subject not found"));
            }
        }

        public override async Task<MessageReponse> CreateSubject(SubjectRequestAdd request,
                                                                 ServerCallContext context)
        {
            var subject = new Subject
            {
                Code = request.Code,
                Name = request.Name,
                NumberSlot = request.NumberSlot
            };
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            var response = new MessageReponse()
            {
                Message = "them mon hoc thanh cong"
            };
            return response;
        }

        public override async Task<MessageReponse> UpdateSubject(SubjectRequestUpdate request,
                                                              ServerCallContext context)
        {
            var subject = await _context.Subjects.FindAsync(request.Id);

            if (subject == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Subject not found"));
            }
            subject.Code = request.Code;
            subject.Name = request.Name;
            subject.NumberSlot = request.NumberSlot;

            await _context.SaveChangesAsync();
            var response = new MessageReponse
            {
                Message = "Updated subject successfully"
            };
            return response;
        }

        public override async Task<MessageReponse> DeleteSubject(SubjectRequest request,
                                                              ServerCallContext context)
        {
            var subject = await _context.Subjects.FindAsync(request.Id);

            if (subject == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Subject not found"));
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            var response = new MessageReponse
            {
                Message = "Deleted subject successfully"
            };

            return response;
        }

        public override async Task<SubjectListResponse> GetAllSubjects(Empty request, ServerCallContext context)
        {
            var subjects = await _context.Subjects.ToListAsync();

            var response = new SubjectListResponse();
            response.Subjects.AddRange(subjects.Select(subject => new SubjectResponse
            {
                Id = subject.Id,
                Code = subject.Code,
                Name = subject.Name,
                NumberSlot = subject.NumberSlot
            }));

            return response;
        }

    }
}

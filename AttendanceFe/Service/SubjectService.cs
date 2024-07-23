using AttendanceFe.Models.Request;
using AttendanceFe.Protos;
using Grpc.Net.Client;
namespace AttendanceFe.Service
{
    public class SubjectService
    {
        private readonly SubjectRPC.SubjectRPCClient _subjectRPCClient;

        public SubjectService()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5095");
            _subjectRPCClient = new SubjectRPC.SubjectRPCClient(channel);
        }

        public async Task CreateSubjetcAsync(sRequest request)
        {
            try
            {
                var grpcRequest = new SubjectRequestAdd
                {
                    Code = request.Code,
                    Name = request.Name,
                    NumberSlot = request.NumberSlot
                };

                var response = await _subjectRPCClient.CreateSubjectAsync(grpcRequest);

                Console.WriteLine("Create successful. Response: " + response.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error create course: " + ex.Message);
            }
        }
    }
}

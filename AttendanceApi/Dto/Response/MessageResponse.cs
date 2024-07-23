using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AttendanceApi.Dto.Response
{
    public class MessageResponse
    {
        public int StatusCode { get; set; } // Mã trạng thái của HTTP Response
        public string Message { get; set; } // Thông điệp lỗi hoặc thông tin phản hồi

        public MessageResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}

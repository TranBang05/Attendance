namespace AttendanceApi.Dto.Response
{
    public class AuthResponse
    {
        public bool Successful { get; set; }
        public string? Error { get; set; }
        public string? Token { get; set; }
    }
}

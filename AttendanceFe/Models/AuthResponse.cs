namespace AttendanceFe.Models
{
    public class AuthResponse
    {
        public bool Successful { get; set; }
        public string? Error { get; set; }
        public string? Token { get; set; }
    }
}

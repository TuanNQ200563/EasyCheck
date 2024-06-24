namespace EasyCheck.Models
{
    public class AttendanceRecord
    {
        public string Id { get; set; } = string.Empty;
        public DateTime CheckinTime { get; set; }
        public string StudentCode { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
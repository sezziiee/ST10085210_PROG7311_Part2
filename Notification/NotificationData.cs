namespace Notification
{
    public class NotificationData
    {
        public int NotificationId { get; set; }
        public string RequestedBy { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsRead { get; set; }
    }
}

namespace Notification
{
    public class StoreNotifications
    {

        private static List<NotificationData> notifications = new List<NotificationData>();
        private static int nextId = 1;

        public static IEnumerable<NotificationData> GetAll() => notifications;

        public static NotificationData Get(int id) => notifications.FirstOrDefault(n => n.NotificationId == id);

        public static void Add(NotificationData notification)
        {
            notification.NotificationId = nextId++;
            notification.DateCreated = DateTime.UtcNow;
            notifications.Add(notification);

        }
    }
}

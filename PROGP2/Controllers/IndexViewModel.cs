using Notification;
using PROGP2.Models;

namespace PROGP2.Controllers
{
    internal class IndexViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<NotificationData> Notifications { get; set; }
    }
}
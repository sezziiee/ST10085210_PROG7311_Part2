using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private static List<NotificationData> lstNotification = new List<NotificationData>();
        private static int nextNotifID = 1;


        
        // GET: api/Notifications
        [HttpGet]
        public IEnumerable<NotificationData> GetNotifications()
        {
            return lstNotification;
        }

        // POST: api/Notifications
        [HttpPost]
        public ActionResult<NotificationData> PostNotification(NotificationData notification)
        {
            notification.NotificationId = nextNotifID++;
            lstNotification.Add(notification);
            return CreatedAtAction(nameof(GetNotifications), new { id = notification.NotificationId }, notification);
        }

        // DELETE: api/Notifications
        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(int id)
        {
            var notification = lstNotification.FirstOrDefault(n => n.NotificationId == id);
            if (notification == null)
            {
                return NotFound();
            }

            lstNotification.Remove(notification);
            return NoContent();
        }
    }
    }

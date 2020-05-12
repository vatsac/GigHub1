using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private DbEntities _context;
        public GigsController()
        {
            _context = new DbEntities();
        }
        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.ID == id && g.Artist_Id == userId);
            if (gig.IsCanceled == true)
                return NotFound();
            gig.IsCanceled = true;
            Notification notification = new Notification
            {
                DateTime = DateTime.Now,
                Type = Convert.ToInt32(NotificationType.GigCanceled),
                Gig_Id = gig.ID


            };
            _context.Notifications.Add(notification);
            _context.SaveChanges();
            int Notificationid = (from record in _context.Notifications
                                  orderby record.Id descending
                                  select record.Id).First();
            var attendeeid = (from s in _context.Attendances
                              where s.GigId == gig.ID
                              select s).ToList();
            foreach (var item in attendeeid)
            {
                UserNotification userNotification = new UserNotification
                {
                    UserId = item.AttendeeId,
                    NotificationId = Notificationid,



                };
                _context.UserNotifications.Add(userNotification);
            }
            _context.SaveChanges();




            return Ok();
        }
    }
}

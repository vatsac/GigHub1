using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{

    [Authorize]
    public class NotificationsController : ApiController
    {



        [HttpGet]
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            using (DbEntities _context = new DbEntities())
            {


                var userId = User.Identity.GetUserId();
                var notifications = _context.UserNotifications
                    .Where(un => un.UserId == userId)
                    .Select(un => un.Notification)
                    .Include(n => n.Gig.AspNetUser)
                    .ToList();
                return notifications.Select(n => new NotificationDto()
                {
                    DateTime = n.DateTime,
                    Gig = new GigDto
                    {
                        Artist = new UserDto
                        {
                            Id = n.Gig.Artist_Id,
                            Name = n.Gig.AspNetUser.Name
                        },
                        DateTime = n.Gig.DateTime,
                        ID = n.Gig.ID,
                        IsCanceled = n.Gig.IsCanceled,
                        Venue = n.Gig.Venue
                    },
                    OriginalDateTime = n.OriginalDateTime,
                    OriginalVenue = n.OriginalVenue,
                    Type = (NotificationType)n.Type

                });

            }

        }

    }
}

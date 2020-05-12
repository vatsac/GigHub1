using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    
    
    public class NotificationsController : ApiController
    {
        private DbEntities _context;
        public NotificationsController()
        {
            _context = new DbEntities();
        }
        
        [HttpGet]
        [Authorize]
        IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.AspNetUser)
                .ToList();
            return notifications.Select(Mapper.Map<Notification, NotificationDto>);

        }

    }
}

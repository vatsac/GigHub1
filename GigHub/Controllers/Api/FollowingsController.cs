using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
   [Authorize] 
    public class FollowingsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Follows(FollowDto dto)
        {
            using(DbEntities _context=new DbEntities())
            {
                var userid = User.Identity.GetUserId();
                if(_context.follows.Any(f=>f.UserId==userid && f.ArtistId == dto.artistId))
                {
                    return BadRequest();
                }
                follow follow = new follow
                {
                    UserId = userid,
                    ArtistId = dto.artistId
                };
                _context.follows.Add(follow);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}

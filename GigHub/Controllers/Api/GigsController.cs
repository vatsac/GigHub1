using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
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
            var gig = _context.Gigs.Single(g => g.ID == id && g.Artist_Id == userId );
            if (gig.IsCanceled==true)
                return NotFound();
            gig.IsCanceled = true;
            _context.SaveChanges();
            return Ok();
        }
    }
}

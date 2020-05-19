using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Gig> gigs { get; set; }
        public bool ShowAction { get; set; }
        public string Heading { get; set; }
        public string SearchTerm { get; set; }
    }
}
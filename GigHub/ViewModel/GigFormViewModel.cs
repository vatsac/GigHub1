using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using GigHub.Controllers;
using GigHub.Models;

namespace GigHub.ViewModel
{
    public class GigFormViewModel
    {
        [Required]
        public string Venue { get; set; }
        [Required]
        [FutureDate]
        public string Date { get; set; }
        [Required]
        [ValidTime]
        public string Time { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public byte GenreID { get; set; }

        public string Heading { get; set; }
        public int id { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<GigsController, ActionResult>> update =
                (c => c.Update(this));
                Expression<Func<GigsController, ActionResult>> create =
                (c => c.Create(this));
                var action = (id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }


        public IEnumerable<Genre> Genres { get; set; }
        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }

    }
}
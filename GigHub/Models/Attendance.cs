//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GigHub.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attendance
    {
        public int Id { get; set; }
        public int GigId { get; set; }
        public string AttendeeId { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Gig Gig { get; set; }
    }
}

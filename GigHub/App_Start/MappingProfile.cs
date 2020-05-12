using AutoMapper;
using GigHub.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Models;


namespace GigHub.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<ApplicationUser, UserDto>();
            Mapper.CreateMap<Gig, GigDto>();
            Mapper.CreateMap<Notification, NotificationDto>();

        }
   

    }
}
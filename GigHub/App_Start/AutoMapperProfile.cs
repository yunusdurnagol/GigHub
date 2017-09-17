using AutoMapper;
using GigHub.Core.Dtos;
using GigHub.Core.Models;

namespace GigHub
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Gig, GigDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Attendance, AttendanceDto>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<Following, FollowingDto>();
        }
    }
}
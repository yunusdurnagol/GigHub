using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;

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
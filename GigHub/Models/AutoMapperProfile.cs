using AutoMapper;
using GigHub.Dtos;

namespace GigHub.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Gig, GigDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Attendance, AttendanceDto>();
            CreateMap<Following, FollowingDto>();
        }
    }
}
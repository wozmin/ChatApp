using AutoMapper;
using ChatServer.Models;
using ChatServer.ViewModel;

namespace ChatServer.MapperProfiles
{
    public class UserMapperProfile:Profile
    {
        public UserMapperProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>();
        }
    }
}

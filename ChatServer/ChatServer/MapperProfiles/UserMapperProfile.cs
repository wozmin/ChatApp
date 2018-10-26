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
            CreateMap<ApplicationUser, ChatUserViewModel>().
                ForMember(uvm=>uvm.AvatarUrl,opt=>opt.MapFrom(u=>u.UserProfile.AvatarUrl));
            CreateMap<UserProfile, UserProfileViewModel>()
                .ForMember(pvm => pvm.UserName, opt => opt.MapFrom(p => p.User.UserName))
                .ForMember(pvm=> pvm.Email,opt=>opt.MapFrom(p=>p.User.Email))
                .ForMember(pvm=>pvm.IsOnline,opt=>opt.MapFrom(p=>p.User.IsOnline));

        }
    }
}

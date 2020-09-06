using AutoMapper;
using ChatServer.ViewModel;
using Services.Dto;

namespace ChatServer.MapperProfiles
{
    /// <summary>
    /// Auth Mapper Profile
    /// </summary>
    public class AuthMapperProfile:Profile
    {
        /// <summary>
        /// Auth Mapper Profile constructor
        /// </summary>
        public AuthMapperProfile()
        {
            CreateMap<RegisterViewModel, RegisterDto>();
        }
    }
}
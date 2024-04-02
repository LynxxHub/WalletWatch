using AutoMapper;
using WalletWatchAPI.DTOs;
using WalletWatchAPI.Models;

namespace WalletWatchAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDTO, User>()
                .ForMember(dest => dest.ProfilePictureURL, opt => opt.MapFrom(src => "/img/avatars/DefaultAvatar.png"));
        }
    }
}
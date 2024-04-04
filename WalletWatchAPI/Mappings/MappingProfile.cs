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

            CreateMap<TransactionCategory, TransactionCategoryDTO>();
            CreateMap<TransactionCategoryDTO, TransactionCategory>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));
        }
    }
}
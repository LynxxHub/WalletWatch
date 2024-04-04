using AutoMapper;
using WalletWatchAPI.DTOs;
using WalletWatchAPI.Models;
using WalletWatchAPI.Models.Enums;

namespace WalletWatchAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDTO, User>()
                .ForMember(dest => dest.ProfilePictureURL, opt => opt.MapFrom(src => "/img/avatars/DefaultAvatar.png"));

            CreateMap<TransactionCategory, TransactionCategoryDTO>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

            CreateMap<TransactionCategoryDTO, TransactionCategory>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse(typeof(TransactionType), src.Type)));
        }
    }
}
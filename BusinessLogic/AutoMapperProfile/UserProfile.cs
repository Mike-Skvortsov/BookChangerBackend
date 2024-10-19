using AutoMapper;
using BusinessLogic.ModelsDTO.UserDTO;
using Database.Models;

namespace BusinessLogic.AutoMapperProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserForChatsDTO>().ForMember(dest => dest.Image,
             opt => opt.MapFrom(src => Convert.ToBase64String(src.Image)));
            CreateMap<LoginDTO, User>();
            CreateMap<User, UserById>()
                .ForMember(dest => dest.Image,
             opt => opt.MapFrom(src => Convert.ToBase64String(src.Image)))
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books))
                .ForMember(dest => dest.ReceivedReviews, opt => opt.MapFrom(src => src.ReceivedReviews));
            CreateMap<User, AddUserInfoDTO>();
            CreateMap<User, UserForBookGetById>();
            CreateMap<AddUserInfoDTO, User>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ConvertImageToBytes(src.Image)))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());
            CreateMap<RegisterDTO, User>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ConvertImageToBytes(src.Image)))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());

            CreateMap<User, OwnerDTO>()
    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
    .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image != null ? Convert.ToBase64String(src.Image) : null));
        }
        private static byte[] ConvertImageToBytes(string imageBase64)
        {
            try
            {
                return !string.IsNullOrWhiteSpace(imageBase64) ? Convert.FromBase64String(imageBase64) : null;
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }
}

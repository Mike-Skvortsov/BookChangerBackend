
using AutoMapper;
using BusinessLogic.ModelsDTO.AuthorDTO;
using Database.Models;

namespace BusinessLogic.AutoMapperProfile
{
    public class AuthorProfile: Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorForDisplayBookDTO>();
            CreateMap<Author, AuthorGetByIdDTO>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(ba => ba.Book).ToList()))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src =>
                Convert.ToBase64String(src.Image)));
            CreateMap<CreateAuthorDTO, Author>()
    .ForMember(
        dest => dest.Image,
        opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.Image) ? Convert.FromBase64String(src.Image) : null)
    );
            CreateMap<Author, GetAuthorsOnPageDTO>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src =>
                Convert.ToBase64String(src.Image)));
        }

    }
}

using AutoMapper;
using BusinessLogic.ModelsDTO.GenreDTO;
using Database.Models;

namespace BusinessLogic.AutoMapperProfile
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<GenreNameDTO, Genre>();
            CreateMap<Genre, GenreNameDTO>();
        }
    }
}

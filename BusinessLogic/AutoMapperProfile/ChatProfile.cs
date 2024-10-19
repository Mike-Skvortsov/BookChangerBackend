using AutoMapper;
using BusinessLogic.ModelsDTO.ChatDTO;
using BusinessLogic.ModelsDTO.Message;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.AutoMapperProfile
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<Chat, ChatDTO>()
            .ForMember(dest => dest.UserOne, opt => opt.MapFrom(src => src.UserOne))
            .ForMember(dest => dest.UserTwo, opt => opt.MapFrom(src => src.UserTwo));
            CreateMap<Chat, CreateChatDTO>();
        }
    }
}

using AutoMapper;
using BusinessLogic.ModelsDTO.GenreDTO;
using BusinessLogic.ModelsDTO.WishlistDTO;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.AutoMapperProfile
{
    public class WishlistProfile: Profile
    {
        public WishlistProfile()
        {
            CreateMap<WishlistCreateDTO, Wishlist>();
        }
    }
}

using AutoMapper;
using BusinessLogic.ModelsDTO.AuthorDTO;
using BusinessLogic.ModelsDTO.BookDTO;
using BusinessLogic.ModelsDTO.GenreDTO;
using BusinessLogic.ModelsDTO.UserDTO;
using Database.Models;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, GetBookByIdDTO>()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src =>
                src.Authors.Select(a => new AuthorForDisplayBookDTO { Name = a.Author.Name, Id = a.Author.Id })))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src =>
                src.Genres.Select(g => new GenreNameDTO { Name = g.Genre.Name, Id = g.Genre.Id })))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src =>
                new UserForBookGetById
                {
                    Id = src.Owner.Id,
                    UserName = src.Owner.UserName,
                    Rating = src.Owner.Rating,
                    Image = src.Owner.Image != null ? Convert.ToBase64String(src.Owner.Image) : null
                }))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src =>
                src.Image != null ? Convert.ToBase64String(src.Image) : null));

        CreateMap<Book, GetAllBooksDTO>()
    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image != null ? Convert.ToBase64String(src.Image) : null))
    .ForMember(dest => dest.AuthorNames, opt => opt.MapFrom(src => string.Join(", ", src.Authors.Select(a => a.Author.Name))))
    .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => new OwnerDTO
    {
        UserName = src.Owner.UserName,
        Rating = src.Owner.Rating,
        Image = src.Owner.Image != null ? Convert.ToBase64String(src.Owner.Image) : null
    }));

        CreateMap<CreateBookDTO, Book>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => DetermineImageFormat(src.Image)))
            .ForMember(dest => dest.Authors, opt => opt.Ignore())
            .ForMember(dest => dest.Genres, opt => opt.Ignore());
        CreateMap<Book, SearchBookDTO>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.AnnouncedPrice, opt => opt.MapFrom(src => src.AnnouncedPrice))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image != null ? Convert.ToBase64String(src.Image) : null))
    .ForMember(dest => dest.AuthorsName, opt => opt.MapFrom(src => string.Join(", ", src.Authors.Select(a => a.Author.Name))));


        CreateMap<Book, BooksForBookcaseDTO>()
            .ForMember(dest => dest.AuthorNames, opt => opt.MapFrom(src => string.Join(", ", src.Authors.Select(a => a.Author.Name))))
            .ForMember(dest => dest.AnnouncedPrice, opt => opt.MapFrom(src => src.AnnouncedPrice))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ConvertImageToBase64String(src.Image)));
    }

    private byte[] DetermineImageFormat(string imageInput)
    {
        if (string.IsNullOrEmpty(imageInput))
        {
            return null;
        }

        try
        {
            // Check if input is a hexadecimal string (it should start with "0x" or only contain hex characters)
            if (imageInput.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                return ConvertHexStringToByteArray(imageInput.Substring(2));
            }
            if (IsHexString(imageInput))
            {
                return ConvertHexStringToByteArray(imageInput);
            }

            // Assume base64 string and convert
            return Convert.FromBase64String(imageInput);
        }
        catch (FormatException ex)
        {
            // Log the exception details here to debug
            Console.WriteLine($"Invalid image input format: {ex.Message}");
            return null; // return null or handle accordingly
        }
    }


    private bool IsHexString(string input)
    {
        // A simple regex to check if a string is a valid hex (may start with "0x")
        return System.Text.RegularExpressions.Regex.IsMatch(input, @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z");
    }

    private byte[] ConvertHexStringToByteArray(string hexString)
    {
        if (hexString.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
        {
            hexString = hexString[2..];
        }

        int numberChars = hexString.Length;
        byte[] bytes = new byte[numberChars / 2];
        for (int i = 0; i < numberChars; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
        }
        return bytes;
    }

    private string ConvertImageToBase64String(byte[] imageBytes)
    {
        if (imageBytes == null || imageBytes.Length == 0) return null;

        return Convert.ToBase64String(imageBytes);
    }
}

using AutoMapper;
using WebApplication1.Dtos;
using WebApplication1.Dtos.GenreDto;
using WebApplication1.Models;

namespace WebApplication1.Profiles
{
    public class GenresProfile: Profile
    {
        public GenresProfile()
        {
            CreateMap<Genre, GenreDto>();
            CreateMap<CreateGenreDto, Genre>();
            CreateMap<UpdateGenreDto, Genre>();
            CreateMap<Genre, UpdateGenreDto>();
        }
    }
}

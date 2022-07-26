using AutoMapper;
using WebApplication1.Dtos.GenreDto;
using WebApplication1.Dtos.NewFolder;
using WebApplication1.Dtos.PlaylistDto;
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


            CreateMap<Song, SongDto>();

            CreateMap<Playlist, PlaylistDto>();
            CreateMap<CreatePlaylistDto, Playlist>();
            CreateMap<UpdatePlaylistDto, Playlist>();
            CreateMap<Playlist, UpdatePlaylistDto>();

        }
    }
}

using AutoMapper;
using WebApplication1.Dtos.NewFolder;
using WebApplication1.Dtos.SongDto;
using WebApplication1.Models;

namespace WebApplication1.Profiles
{
    public class SongsProfile: Profile
    {
        public SongsProfile()
        {
            CreateMap<Song, SongDtoToView>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Album, opt => opt.MapFrom(src => src.Album.Name))
                .ForMember(dest => dest.Playlist, opt => opt.MapFrom(src => src.Playlist.Name));
            CreateMap<CreateSongDto, Song>();
            CreateMap<UpdateSongDto, Song>();
            CreateMap<Song, UpdateSongDto>();
        }
    }
}

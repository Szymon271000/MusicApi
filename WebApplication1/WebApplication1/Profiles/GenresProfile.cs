using AutoMapper;
using WebApplication1.Dtos;
using WebApplication1.Dtos.AlbumDto;
using WebApplication1.Dtos.ArtistDto;
using WebApplication1.Dtos.GenreDto;
using WebApplication1.Dtos.NewFolder;
using WebApplication1.Dtos.PlaylistDto;
using WebApplication1.Dtos.SongDto;
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


            CreateMap<Song, SongDtoToView>();
            CreateMap<CreateSongDto, Song>();
            CreateMap<UpdateSongDto, Song>();
            CreateMap<Song, UpdateSongDto>();

            CreateMap<Playlist, PlaylistDto>();
            CreateMap<CreatePlaylistDto, Playlist>();
            CreateMap<UpdatePlaylistDto, Playlist>();
            CreateMap<Playlist, UpdatePlaylistDto>();

            CreateMap<Album, AlbumDtoToView>();
            CreateMap<CreatedAlbumDto, Album>();
            CreateMap<UpdatedAlbumDto, Album>();
            CreateMap<Album,UpdatedAlbumDto>();

            CreateMap<Artist, ArtistDtoToView>();
            CreateMap<CreateArtistDto, Artist>();
            CreateMap<UpdateArtistDto, Artist>();
            CreateMap<Artist,UpdateArtistDto>();
        }
    }
}

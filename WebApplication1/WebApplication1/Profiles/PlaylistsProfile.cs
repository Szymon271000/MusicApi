using AutoMapper;
using WebApplication1.Dtos.PlaylistDto;
using WebApplication1.Models;

namespace WebApplication1.Profiles
{
    public class PlaylistsProfile: Profile
    {
        public PlaylistsProfile()
        {

            CreateMap<Playlist, PlaylistDto>();
            CreateMap<CreatePlaylistDto, Playlist>();
            CreateMap<UpdatePlaylistDto, Playlist>();
            CreateMap<Playlist, UpdatePlaylistDto>();

        }
    }
}

using AutoMapper;
using WebApplication1.Dtos.AlbumDto;
using WebApplication1.Models;

namespace WebApplication1.Profiles
{
    public class AlbumsProfile: Profile
    {
        public AlbumsProfile()
        {
            CreateMap<Album, AlbumDtoToView>();
            CreateMap<CreatedAlbumDto, Album>();
            CreateMap<UpdatedAlbumDto, Album>();
            CreateMap<Album, UpdatedAlbumDto>();
        }
    }
}

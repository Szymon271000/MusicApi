using AutoMapper;
using WebApplication1.Dtos;
using WebApplication1.Dtos.ArtistDto;
using WebApplication1.Models;

namespace WebApplication1.Profiles
{
    public class ArtistsProfile: Profile
    {
        public ArtistsProfile()
        {
            CreateMap<Artist, ArtistDtoToView>();
            CreateMap<CreateArtistDto, Artist>();
            CreateMap<UpdateArtistDto, Artist>();
            CreateMap<Artist, UpdateArtistDto>();
        }
    }
}

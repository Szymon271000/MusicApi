using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Repository.Interface;
using WebApplication1.Dtos;
using WebApplication1.Dtos.ArtistDto;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Artist> _artistRepository;
        private readonly IBaseRepository<Album> _albumRepository;


        public ArtistController(IBaseRepository<Artist> artistRepository, IBaseRepository<Album> albumRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArtists()
        {
            var artists = await _artistRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<ArtistDtoToView>>(artists));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtistById(int id)
        {
            var artist = await _artistRepository.GetById(id);
            if (artist == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ArtistDtoToView>(artist));
        }

        [HttpPost]
        public async Task<IActionResult> CreateArtist(Dtos.ArtistDto.CreateArtistDto createdArtist)
        {
            if (ModelState.IsValid)
            {
                var artistToAdd = _mapper.Map<Artist>(createdArtist);
                await _artistRepository.Add(artistToAdd);
                return Ok(_mapper.Map<ArtistDtoToView>(artistToAdd));
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtist(int id, UpdateArtistDto updatedArtist)
        {
            var artist = await _artistRepository.GetById(id);
            if (artist == null)
            {
                return NotFound();
            }
            _mapper.Map(updatedArtist, artist);
            await _artistRepository.Update(artist);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artistToDelete = await _artistRepository.GetById(id);
            if (artistToDelete == null)
            {
                return NotFound();
            }
            await _artistRepository.Delete(artistToDelete);
            return NoContent();
        }

        [HttpPut("{id}/albums/{albumId}")]
        public async Task<IActionResult> AddAlbumToArtist(int id, int albumId)
        {
            var artist = await _artistRepository.GetById(id);
            if (artist == null)
            {
                return NotFound();
            }
            var album = await _albumRepository.GetById(albumId);
            if (album == null)
            {
                return NotFound();
            }

            artist.Albums.Add(album);
            await _albumRepository.Update(album);
            await _artistRepository.Update(artist);
            await _albumRepository.Save();
            await _artistRepository.Save();
            return Ok();
        }
    }
}

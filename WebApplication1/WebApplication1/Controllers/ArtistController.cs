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
        private readonly IBaseRepository<Song> _songRepository;


        public ArtistController(IBaseRepository<Artist> artistRepository, IBaseRepository<Song> songRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _songRepository = songRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var artists = await _artistRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<ArtistDtoToView>>(artists));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
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
        public async Task<IActionResult> UpdateGenre(int id, UpdateArtistDto updatedArtist)
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

            var songsOfThisArtist = await _songRepository.GetAll();
            foreach (var song in songsOfThisArtist)
            {
                if (song.ArtistId == id)
                {
                    song.Genre = null;
                }
            }
            await _artistRepository.Delete(artistToDelete);
            return NoContent();
        }
    }
}

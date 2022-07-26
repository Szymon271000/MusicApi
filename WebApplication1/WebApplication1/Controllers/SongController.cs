using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Repository.Interface;
using WebApplication1.Dtos.NewFolder;
using WebApplication1.Dtos.SongDto;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly IBaseRepository<Song> _songRepository;
        private readonly IBaseRepository<Artist> _artistRepository;
        private readonly IBaseRepository<Album> _albumRepository;
        private readonly IBaseRepository<Genre> _genreRepository;



        private readonly IMapper _mapper;
        public SongController(IBaseRepository<Song> songRepository, IBaseRepository<Artist> artistRepository, IBaseRepository<Album> albumRepository, IBaseRepository<Genre> genreRepository, IMapper mapper)
        {
            _songRepository = songRepository;
            _genreRepository = genreRepository;
            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSongs()
        {
            var songs = await _songRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<SongDtoToView>>(songs));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSongById(int id)
        {
            var song = await _songRepository.GetById(id);
            if (song == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SongDtoToView>(song));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSong(CreateSongDto createdSong)
        {
            if (ModelState.IsValid)
            {
                var songToAdd = _mapper.Map<Song>(createdSong);
                await _songRepository.Add(songToAdd);
                return Ok(_mapper.Map<SongDtoToView>(songToAdd));
            }
            return BadRequest();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSong(int id, UpdateSongDto updatedSong)
        {
            var song = await _songRepository.GetById(id);
            if (song == null)
            {
                return NotFound();
            }
            _mapper.Map(updatedSong, song);
            await _songRepository.Update(song);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var songToDelete = await _songRepository.GetById(id);
            if (songToDelete == null)
            {
                return NotFound();
            }

            await _songRepository.Delete(songToDelete);
            return NoContent();
        }

        [HttpPut("{id}/artist/{artistId}")]
        public async Task<IActionResult> UpdateArtistOfSong(int id, int artistId)
        {
            var song = await _songRepository.GetById(id);
            if (song == null)
            {
                return NotFound();
            }
            var artist = await _artistRepository.GetById(artistId);
            if (artist == null)
            {
                return NotFound();
            }
            song.ArtistId = artist.Id;
            await _songRepository.Update(song);
            return Ok();
        }

        [HttpPut("{id}/album/{albumId}")]
        public async Task<IActionResult> UpdateAlbumtOfSong(int id, int albumId)
        {
            var song = await _songRepository.GetById(id);
            if (song == null)
            {
                return NotFound();
            }
            var album = await _albumRepository.GetById(albumId);
            if (album == null)
            {
                return NotFound();
            }
            song.AlbumId = album.Id;
            await _songRepository.Update(song);
            return Ok();
        }

        [HttpPut("{id}/genre/{genreId}")]
        public async Task<IActionResult> UpdateGenreOfSong(int id, int genreId)
        {
            var song = await _songRepository.GetById(id);
            if (song == null)
            {
                return NotFound();
            }
            var genre = await _genreRepository.GetById(genreId);
            if (genre == null)
            {
                return NotFound();
            }
            song.GenreId = genre.Id;
            await _songRepository.Update(song);
            return Ok();
        }
    }
}

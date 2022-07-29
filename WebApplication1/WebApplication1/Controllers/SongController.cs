using AutoMapper;
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
        private readonly IBaseRepository<Genre> _genreRepository;



        private readonly IMapper _mapper;
        public SongController(IBaseRepository<Song> songRepository, IBaseRepository<Genre> genreRepository, IMapper mapper)
        {
            _songRepository = songRepository;
            _genreRepository = genreRepository;

            _mapper = mapper;
        }

        /// <summary>
        /// Get all songs
        /// </summary>
        /// <returns>All songs</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///     {
        ///        "name": "",
        ///        "Albumid: "",
        ///        "Genreid:" ""
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns all songs</response>
        /// <response code="400">If the item is null</response>
        [HttpGet]
        public async Task<IActionResult> GetAllSongs()
        {
            var songs = await _songRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<SongDtoToView>>(songs));
        }

        /// <summary>
        /// Get song with specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>All songs</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///     {
        ///        "name": "",
        ///        "Albumid: "",
        ///        "Genreid:" ""
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns song with specific id</response>
        /// <response code="400">If the item is null</response>
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

        /// <summary>
        /// Create song
        /// </summary>
        /// <param name="createdSong"></param>
        /// <returns>Created song</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///     {
        ///        "name": "",
        ///        "Albumid: "",
        ///        "Genreid:" ""
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Ok</response>
        /// <response code="400">Bad Request</response>
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
        /// <summary>
        /// Update song
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedSong"></param>
        /// <returns>Update song</returns>
        /// <remarks>
        /// </remarks>
        /// <response code="201">Ok</response>
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

        /// <summary>
        /// Delete song
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete song</returns>
        /// </remarks>
        /// <response code="201">NoContent</response>
        /// <response code="404">Not found</response>
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

        /// <summary>
        /// Update Genre of song
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genreId"></param>
        /// <returns>Update Genre of song</returns>
        /// <response code="201">Ok</response>
        /// <response code="404">Not found</response>
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
            song.Genre = genre;
            await _genreRepository.Update(genre);
            await _songRepository.Update(song);
            return Ok();
        }
    }
}

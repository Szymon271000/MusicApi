using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Repository.Interface;
using WebApplication1.Dtos.GenreDto;
using WebApplication1.Dtos.NewFolder;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IBaseRepository<Genre> _genreRepository;
        private readonly IBaseRepository<Song> _songRepository;

        private readonly IMapper _mapper;
        public GenreController(IBaseRepository<Genre> genreRepository, IBaseRepository<Song> songRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _songRepository = songRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all genres
        /// </summary>
        /// <returns>All genres</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///     {
        ///        "name": "",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns all genres</response>
        /// <response code="400">If the item is null</response>
        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<GenreDto>>(genres));
        }

        /// <summary>
        /// Get a genre by specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A genre with this id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///     {
        ///        "name": "",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the genre</response>
        /// <response code="400">If the item is null</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var genre = await _genreRepository.GetById(id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GenreDto>(genre));
        }


        /// <summary>
        /// Get songs by this specific gender
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Song of this gender with this id</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET
        ///     {
        ///        "name": "Song1",
        ///        "albumId": 3,
        ///        "genreId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the songs</response>
        /// <response code="400">If the item is null</response>
    [HttpGet("{id}/songs")]
        public async Task<IActionResult> GetSongOfThisGenre(int id)
        {
            var genre = await _genreRepository.GetById(id);
            if (genre == null)
            {
                return NotFound();
            }

            var songs = _songRepository.GetAll().Result.Where(x => x.GenreId == genre.Id);
            return Ok(_mapper.Map<IEnumerable<SongDtoToView>>(songs));
        }

        /// <summary>
        /// Get song by this specific gender by specif id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="songId"></param>
        /// <returns>Song of this gender with this specific id</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET
        ///     {
        ///        "name": "Song1",
        ///        "albumId": 3,
        ///        "genreId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the songs</response>
        /// <response code="400">If the item is null</response>
        [HttpGet("{id}/songs/{songId}")]
        public async Task<IActionResult> GetSongOfThisGenre(int id, int songId)
        {
            var genre = await _genreRepository.GetById(id);
            if (genre == null)
            {
                return NotFound();
            }

            var song = _songRepository.GetAll().Result.FirstOrDefault(x => x.GenreId == genre.Id && x.Id == songId);

            if (song == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SongDtoToView>(song));
        }

        /// <summary>
        /// Create gender
        /// </summary>
        /// <param name="createdGenre"></param>
        /// <returns>Create genre</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///     {
        ///        "name": "",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Ok</response>
        /// <response code="400">Bad Request</response>

        [HttpPost]
        public async Task<IActionResult> CreateGenre(CreateGenreDto createdGenre)
        {
            if(ModelState.IsValid)
            {
                 var genreToAdd = _mapper.Map<Genre>(createdGenre);
                 await _genreRepository.Add(genreToAdd);
                return Ok(_mapper.Map<GenreDto>(genreToAdd));
            }
            return BadRequest();
            
        }

        /// <summary>
        /// Update gender
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedGenre"></param>
        /// <returns>Update genre</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///     {
        ///        "name": "",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Ok</response>
        /// <response code="404">Not found</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, UpdateGenreDto updatedGenre)
        {
            var genre = await _genreRepository.GetById(id);
            if (genre == null)
            {
                return NotFound();
            }
            _mapper.Map(updatedGenre, genre);
            await _genreRepository.Update(genre);
            return Ok();
        }

        /// <summary>
        /// Delete gender
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete genre</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///     {
        ///        "name": "",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Ok</response>
        /// <response code="404">Not found</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genreToDelete = await _genreRepository.GetById(id);
            if (genreToDelete == null)
            {
                return NotFound();
            }

            var songsWithThisGender = await _songRepository.GetAll();
            foreach (var song in songsWithThisGender)
            {
                if (song.GenreId == id)
                {
                    song.Genre = null;
                }
            }
            await _genreRepository.Delete(genreToDelete);
            return NoContent();
        }

        /// <summary>
        /// Update Song gender
        /// </summary>
        /// <param name="id"></param>
        /// <param name="songId"></param>
        /// <returns>Update Song gender</returns>
        /// <response code="201">Ok</response>
        /// <response code="404">Not found</response>

        [HttpPut("{id}/addGender/{songId}")]
        public async Task<IActionResult> UpdateSongGender(int id, int songId)
        {
            var genre = await _genreRepository.GetById(id);
            if (genre == null)
            {
                return NotFound();
            }
            var song = await _songRepository.GetById(songId);
            if (song == null)
            {
                return NotFound();
            }
            song.Genre = genre;
            await _songRepository.Update(song);
            await _genreRepository.Update(genre);
            await _songRepository.Save();
            await _genreRepository.Save();
            return Ok();
        }
    }
}

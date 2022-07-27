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

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<GenreDto>>(genres));
        }

        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
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

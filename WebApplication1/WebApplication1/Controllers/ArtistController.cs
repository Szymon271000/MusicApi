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

        /// <summary>
        /// Get all artists
        /// </summary>
        /// <returns>All artist</returns>
        /// <remarks>
        /// Sample request:
        ///GET
        ///{
        ///  "name": "",
        ///  "albums": [
        ///    {
        ///      "name": "",
        ///      "songs": [
        ///        {
        ///          "name": "",
        ///          "albumId": ,
        ///          "genreId": 
        ///        },
        ///        {
        ///          "name": ,
        ///          "albumId": ,
        ///          "genreId": 
        ///        }
        ///      ]
        ///    }
        ///  ]
        ///}
        ///
        /// </remarks>
        /// <response code="201">Returns all genres</response>
        /// <response code="400">If the item is null</response>

        [HttpGet]
        public async Task<IActionResult> GetAllArtists()
        {
            var artists = await _artistRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<ArtistDtoToView>>(artists));
        }

        /// <summary>
        /// Get artist with this id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Artist with this id</returns>
        /// <remarks>
        /// Sample request:
        /// {
        ///"name": "",
        ///"songs": [
        ///    {
        ///    "name": "Song2",
        ///    "albumId": 2,
        ///    "genreId": 1
        ///    },
        ///    {
        ///    "name": "Song3",
        ///    "albumId": 2,
        ///    "genreId": 1
        ///    }
        ///]
        ///},
        /// </remarks>
        /// <response code="201">Returns artist with this id</response>
        /// <response code="400">If the item is null</response>
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

        /// <summary>
        /// Create new artist
        /// </summary>
        /// <param name="createdArtist"></param>
        /// <returns>New artist</returns>
        /// <remarks>
        /// Sample request:
        ///   {
       ///"name": "Artist3",
      ///"albums": []
      ///  }
    /// </remarks>
    /// <response code="201">Create new artist</response>
    /// <response code="400">If the item is null</response>
    [HttpPost]
        public async Task<IActionResult> CreateArtist(CreateArtistDto createdArtist)
        {
            if (ModelState.IsValid)
            {
                var artistToAdd = _mapper.Map<Artist>(createdArtist);
                await _artistRepository.Add(artistToAdd);
                return Ok(_mapper.Map<ArtistDtoToView>(artistToAdd));
            }
            return BadRequest();
        }

        /// <summary>
        /// Update an exiting artist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedArtist"></param>
        /// <returns>updatede artist</returns>
        /// <remarks>
        /// Sample request:
        ///   {
        ///     "name": "Artist3",
        ///     "albums": []
        ///  }
        /// </remarks>
        /// <response code="201">Updated artist</response>
        /// <response code="400">If the item is null</response>
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

        /// <summary>
        /// Delete an exiting artist
        /// </summary>
        /// <param name="id"></param>
        /// <response code="201">Delete artist</response>
        /// <response code="400">If the item is null</response>
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


        /// <summary>
        /// Add album to artist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="albumId"></param>
        /// <returns>Add album to artist</returns>
        /// <response code="201">Ok</response>
        /// <response code="404">Not found</response>
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

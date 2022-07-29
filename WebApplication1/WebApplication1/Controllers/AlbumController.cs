using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Repository.Interface;
using WebApplication1.Dtos.AlbumDto;
using WebApplication1.Dtos.NewFolder;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IBaseRepository<Album> _albumRepository;
        private readonly IBaseRepository<Song> _songRepository;

        private readonly IMapper _mapper;

        public AlbumController(IBaseRepository<Album> albumRepository, IBaseRepository<Song> songRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _songRepository = songRepository;
            _mapper = mapper;
        }



        /// <summary>
        /// Get all albums
        /// </summary>
        /// <returns>All albums</returns>
        /// <remarks>
        /// Sample request:
        /// {
        ///"name": "Album2",
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
        /// <response code="201">Returns all albums</response>
        /// <response code="400">If the item is null</response>
        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var albums = await _albumRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<AlbumDtoToView>>(albums));
        }

        /// <summary>
        /// Get album with this id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Album with this id</returns>
        /// <remarks>
        /// Sample request:
        /// {
        ///"name": "Album2",
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
        /// <response code="201">Returns album with this id</response>
        /// <response code="400">If the item is null</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbumById(int id)
        {
            var album = await _albumRepository.GetById(id);
            if (album == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AlbumDtoToView>(album));
        }

        /// <summary>
        /// Get songs of this album with this id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Songs of this album with this id</returns>
        /// <remarks>
        /// Sample request:
        /// {
        ///"name": "Album2",
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
        /// <response code="201">Returns songs of album with this id</response>
        /// <response code="400">If the item is null</response>

        [HttpGet("{id}/songs")]
        public async Task<IActionResult> GetSongOfThisAlbum(int id)
        {
            var album = await _albumRepository.GetById(id);
            if (album == null)
            {
                return NotFound();
            }

            var songs = album.Songs;
            return Ok(_mapper.Map<IEnumerable<SongDtoToView>>(songs));
        }

        /// <summary>
        /// Get song with this id of this album with this id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="songId"></param>
        /// <returns>Song with this id of this album with this id</returns>
        /// <remarks>
        /// Sample request:
        /// {
        ///"name": "Album2",
        ///"songs": [
        ///    {
        ///    "name": "Song2",
        ///    "albumId": 2,
        ///    "genreId": 1
        ///]
        ///},
        /// </remarks>
        /// <response code="201">Song with this id of this album with this id</response>
        /// <response code="400">If the item is null</response>
        [HttpGet("{id}/songs/{songId}")]
        public async Task<IActionResult> GetSongOfThisAlbum(int id, int songId)
        {
            var album = await _albumRepository.GetById(id);
            if (album == null)
            {
                return NotFound();
            }

            var song = album.Songs.FirstOrDefault(x => x.Id == songId);

            if (song == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SongDtoToView>(song));
        }

        /// <summary>
        /// Create an album
        /// </summary>
        /// <param name="createdAlbum"></param>
        /// <returns>New album</returns>
        /// <remarks>
        /// Sample request:
        ///{
        ///{
        ///  "name": "Album4",
        ///  "songs": []
        ///}
        /// </remarks>
        /// <response code="201">New album in database</response>
    [HttpPost]
        public async Task<IActionResult> CreateAlbum(CreatedAlbumDto createdAlbum)
        {
            if (ModelState.IsValid)
            {
                var albumToAdd = _mapper.Map<Album>(createdAlbum);
                await _albumRepository.Add(albumToAdd);
                return Ok(_mapper.Map<AlbumDtoToView>(albumToAdd));
            }
            return BadRequest();
        }

        /// <summary>
        /// Update an album
        /// </summary>
        /// <param name="updatedAlbum"></param>
        /// <param name="id"></param>
        /// <returns>Updated album</returns>
        /// <response code="201">Updated album in database</response>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlbum(int id, UpdatedAlbumDto updatedAlbum)
        {
            var album = await _albumRepository.GetById(id);
            if (album == null)
            {
                return NotFound();
            }
            _mapper.Map(updatedAlbum, album);
            await _albumRepository.Update(album);
            return Ok();
        }

        /// <summary>
        /// Delete album
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete album</returns>
        /// <response code="201">NoContent</response>
        /// <response code="404">Not found</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var albumToDelete = await _albumRepository.GetById(id);
            if (albumToDelete == null)
            {
                return NotFound();
            }
            await _albumRepository.Delete(albumToDelete);
            return NoContent();
        }

        /// <summary>
        /// Add song to Album
        /// </summary>
        /// <param name="id"></param>
        /// <param name="songId"></param>
        /// <returns>Add song to Album</returns>
        /// <response code="201">Ok</response>
        /// <response code="404">Not found</response>

        [HttpPut("{id}/songs/{songId}")]
        public async Task<IActionResult> AddSongToAlbum(int id, int songId)
        {
            var album = await _albumRepository.GetById(id);
            if (album == null)
            {
                return NotFound();
            }
            var song = await _songRepository.GetById(songId);
            if (song == null)
            {
                return NotFound();
            }

            album.Songs.Add(song);

            await _songRepository.Update(song);
            await _albumRepository.Update(album);
            await _songRepository.Save();
            await _albumRepository.Save();
            return Ok();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Repository.Interface;
using WebApplication1.Dtos.NewFolder;
using WebApplication1.Dtos.PlaylistDto;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IBaseRepository<Playlist> _playlistRepository;
        private readonly IBaseRepository<Song> _songRepository;



        private readonly IMapper _mapper;
        public PlaylistController(IBaseRepository<Playlist> playlistRepository, IBaseRepository<Song> songRepository, IMapper mapper)
        {
            _songRepository = songRepository;
            _playlistRepository = playlistRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all playlists
        /// </summary>
        /// <returns>All playlists</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///        [
        ///          {
        ///    "name": "Playlist1",
        ///            "songs": [
        ///              {
        ///        "name": "Song1",
        ///                "albumId": 3,
        ///                "genreId": 1
        ///      },
        ///      {
        ///        "name": "Song2",
        ///        "albumId": 2,
        ///        "genreId": 1
        ///      },
        ///      {
        ///    "name": "Song3",
        ///        "albumId": 2,
        ///        "genreId": 1
        ///      }
        ///    ]
        ///  }
        ///]
        /// </remarks>
        /// <response code="201">Returns all playlists</response>
        /// <response code="400">If the item is null</response>
        [HttpGet]
        public async Task<IActionResult> GetAllPlaylists()
        {
            var playlists = await _playlistRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<PlaylistDto>>(playlists));
        }

        /// <summary>
        /// Get playlist with specific id 
        /// </summary>
        /// <returns>Playlist with specific id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///        [
        ///          {
        ///    "name": "Playlist1",
        ///            "songs": [
        ///              {
        ///        "name": "Song1",
        ///                "albumId": 3,
        ///                "genreId": 1
        ///      },
        ///      {
        ///        "name": "Song2",
        ///        "albumId": 2,
        ///        "genreId": 1
        ///      },
        ///      {
        ///    "name": "Song3",
        ///        "albumId": 2,
        ///        "genreId": 1
        ///      }
        ///    ]
        ///  }
        ///]
        /// </remarks>
        /// <response code="201">Returns playlist with specific id</response>
        /// <response code="400">If the item is null</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaylistById(int id)
        {
            var playlist = await _playlistRepository.GetById(id);
            if (playlist == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PlaylistDto>(playlist));
        }

        /// <summary>
        /// Get songs of playlist with specific id 
        /// </summary>
        /// <returns>songs of playlist with specific id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///        [
        ///          {
        ///    "name": "Playlist1",
        ///            "songs": [
        ///              {
        ///        "name": "Song1",
        ///                "albumId": 3,
        ///                "genreId": 1
        ///      },
        ///      {
        ///        "name": "Song2",
        ///        "albumId": 2,
        ///        "genreId": 1
        ///      },
        ///      {
        ///    "name": "Song3",
        ///        "albumId": 2,
        ///        "genreId": 1
        ///      }
        ///    ]
        ///  }
        ///]
        /// </remarks>
        /// <response code="201">Returns songs of playlist with specific id</response>
        /// <response code="400">If the item is null</response>
        [HttpGet("{id}/songs")]
        public async Task<IActionResult> GetSongOfThisPlaylist(int id)
        {
            var playlist = await _playlistRepository.GetById(id);
            if (playlist == null)
            {
                return NotFound();
            }

            var songs = playlist.Songs.ToList();
            return Ok(_mapper.Map<IEnumerable<SongDtoToView>>(songs));
        }


        /// <summary>
        /// Get song with specific id of playlist with specific id 
        /// </summary>
        /// <returns>song of playlist with specific id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///        [
        ///          {
        ///    "name": "Playlist1",
        ///            "songs": [
        ///              {
        ///        "name": "Song1",
        ///                "albumId": 3,
        ///                "genreId": 1
        ///      },
        ///    ]
        ///  }
        ///]
        /// </remarks>
        /// <response code="201">Returns song with specific id of playlist with specific id</response>
        /// <response code="400">If the item is null</response>
        [HttpGet("{id}/songs/{songId}")]
        public async Task<IActionResult> GetSongOfThisPlaylist(int id, int songId)
        {
            var playlist = await _playlistRepository.GetById(id);
            if (playlist == null)
            {
                return NotFound();
            }

            var song = playlist.Songs.FirstOrDefault(x=> x.Id== songId);

            if (song == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SongDtoToView>(song));
        }

        /// <summary>
        /// Create playlist
        /// </summary>
        /// <param name="createdPlaylist"></param>
        /// <returns>Create playlist</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///{
        ///  "name": "",
        ///  "songs": []
        ///}
        ///
        /// </remarks>
        /// <response code="201">Ok</response>
        /// <response code="400">Bad Request</response>
    [HttpPost]
        public async Task<IActionResult> CreatePlaylist(CreatePlaylistDto createdPlaylist)
        {
            if (ModelState.IsValid)
            {
                var playlistToAdd = _mapper.Map<Playlist>(createdPlaylist);
                await _playlistRepository.Add(playlistToAdd);
                return Ok(_mapper.Map<PlaylistDto>(playlistToAdd));
            }
            return BadRequest();
        }

        /// <summary>
        /// Update playlist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedPlaylist"></param>
        /// <returns>Update playlist</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///{
        ///  "name": "",
        ///  "songs": []
        ///}
        ///
        /// </remarks>
        /// <response code="201">Ok</response>
        /// <response code="400">Bad Request</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaylist(int id, UpdatePlaylistDto updatedPlaylist)
        {
            var playlist = await _playlistRepository.GetById(id);
            if (playlist == null)
            {
                return NotFound();
            }
            _mapper.Map(updatedPlaylist, playlist);
            await _playlistRepository.Update(playlist);
            return Ok();
        }

        /// <summary>
        /// Delete playlist
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete playlist</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET 
        ///     {
        ///        "name": "",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">NoContent</response>
        /// <response code="404">Not found</response>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            var playlistToDelete = await _playlistRepository.GetById(id);
            if (playlistToDelete == null)
            {
                return NotFound();
            }
            await _playlistRepository.Delete(playlistToDelete);
            return NoContent();
        }


        /// <summary>
        /// Add song to playlist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="songId"></param>
        /// <returns>Add song to playlist</returns>
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
        [HttpPut("{id}/songs/{songId}")]
        public async Task<IActionResult> AddSongToPlaylist(int id, int songId)
        {
            var playlist = await _playlistRepository.GetById(id);
            if (playlist == null)
            {
                return NotFound();
            }
            var song = await _songRepository.GetById(songId);
            if (song == null)
            {
                return NotFound();
            }
            
            playlist.Songs.Add(song);
            await _songRepository.Update(song);
            await _playlistRepository.Update(playlist);
            await _playlistRepository.Save();
            await _songRepository.Save();
            return Ok();
        }
    }
}

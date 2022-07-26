using AutoMapper;
using Microsoft.AspNetCore.Http;
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



        private readonly IMapper _mapper;
        public PlaylistController(IBaseRepository<Playlist> playlistRepository, IMapper mapper)
        {
            _playlistRepository = playlistRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlaylists()
        {
            var playlists = await _playlistRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<PlaylistDto>>(playlists));
        }

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

        [HttpGet("{id}/songs")]
        public async Task<IActionResult> GetSongOfThisPlaylist(int id)
        {
            var playlist = await _playlistRepository.GetById(id);
            if (playlist == null)
            {
                return NotFound();
            }

            var songs = playlist.Songs.ToList();
            return Ok(_mapper.Map<IEnumerable<SongDto>>(songs));
        }

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
            return Ok(_mapper.Map<SongDto>(song));
        }

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
    }
}

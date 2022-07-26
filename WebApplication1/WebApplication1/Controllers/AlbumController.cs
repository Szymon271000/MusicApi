using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var albums = await _albumRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<AlbumDto>>(albums));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetgenresById(int id)
        {
            var album = await _albumRepository.GetById(id);
            if (album == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AlbumDto>(album));
        }

        [HttpGet("{id}/songs")]
        public async Task<IActionResult> GetSongOfThisAlbum(int id)
        {
            var album = await _albumRepository.GetById(id);
            if (album == null)
            {
                return NotFound();
            }

            var songs = _songRepository.GetAll().Result.Where(x => x.AlbumId == album.Id);
            return Ok(_mapper.Map<IEnumerable<SongDto>>(songs));
        }

        [HttpGet("{id}/songs/{songId}")]
        public async Task<IActionResult> GetSongOfThisAlbum(int id, int songId)
        {
            var album = await _albumRepository.GetById(id);
            if (album == null)
            {
                return NotFound();
            }

            var song = _songRepository.GetAll().Result.FirstOrDefault(x => x.AlbumId == album.Id && x.Id == songId);

            if (song == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SongDto>(song));
        }


        [HttpPost]
        public async Task<IActionResult> CreateAlbum(CreatedAlbumDto createdAlbum)
        {
            if (ModelState.IsValid)
            {
                var albumToAdd = _mapper.Map<Album>(createdAlbum);
                await _albumRepository.Add(albumToAdd);
                return Ok(_mapper.Map<AlbumDto>(albumToAdd));
            }
            return BadRequest();
        }

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var albumToDelete = await _albumRepository.GetById(id);
            if (albumToDelete == null)
            {
                return NotFound();
            }

            var songsWithThisAlbum = await _songRepository.GetAll();
            foreach (var song in songsWithThisAlbum)
            {
                if (song.AlbumId == albumToDelete.Id)
                {
                    song.Album = null;
                }
            }
            await _albumRepository.Delete(albumToDelete);
            return NoContent();
        }
    }
}

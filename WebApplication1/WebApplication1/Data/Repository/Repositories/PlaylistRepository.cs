using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Repository.Interface;
using WebApplication1.Models;

namespace WebApplication1.Data.Repository.Repositories
{
    public class PlaylistRepository : IBaseRepository<Playlist>
    {
        private readonly MusicDbContext _musicDbContext;

        public PlaylistRepository(MusicDbContext context)
        {
            _musicDbContext = context;
        }
        public async Task Add(Playlist entity)
        {
            await _musicDbContext.Playlists.AddAsync(entity);
            await Save();
        }

        public async Task Delete(Playlist entity)
        {
            _musicDbContext.Playlists.Remove(entity);
            await Save();
        }

        public async Task<List<Playlist>> GetAll()
        {
            return await _musicDbContext.Playlists.Include(x=>x.Songs).ToListAsync();

        }

        public async Task<Playlist> GetById(int id)
        {
            return await _musicDbContext.Playlists.Include(x=> x.Songs).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save()
        {
            await _musicDbContext.SaveChangesAsync();
        }

        public async Task Update(Playlist entity)
        {
            _musicDbContext.Playlists.Update(entity);
            await Save();
        }
    }
}

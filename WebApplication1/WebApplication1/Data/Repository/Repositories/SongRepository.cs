using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Repository.Interface;
using WebApplication1.Models;

namespace WebApplication1.Data.Repository.Repositories
{
    public class SongRepository : IBaseRepository<Song>
    {
        private readonly MusicDbContext _musicDbContext;

        public SongRepository(MusicDbContext context)
        {
            _musicDbContext = context;
        }
        public async Task Add(Song entity)
        {
            await _musicDbContext.Songs.AddAsync(entity);
            await Save();
        }

        public async Task Delete(Song entity)
        {
            _musicDbContext.Songs.Remove(entity);
            await Save();
        }

        public async Task<List<Song>> GetAll()
        {
            return await _musicDbContext.Songs.ToListAsync();
        }

        public async Task<Song> GetById(int id)
        {
            return await _musicDbContext.Songs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save()
        {
            await _musicDbContext.SaveChangesAsync();
        }

        public async Task Update(Song entity)
        {
            _musicDbContext.Songs.Update(entity);
            await Save();
        }
    }
}

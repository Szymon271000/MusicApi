using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Repository.Interface;
using WebApplication1.Models;

namespace WebApplication1.Data.Repository.Repositories
{
    public class AlbumRepository : IBaseRepository<Album>
    {
        private readonly MusicDbContext _musicDbContext;

        public AlbumRepository(MusicDbContext context)
        {
            _musicDbContext = context;
        }

        public async Task Add(Album entity)
        {
            await _musicDbContext.Albums.AddAsync(entity);
            await Save();
        }

        public async Task Delete(Album entity)
        {
            _musicDbContext.Albums.Remove(entity);
            await Save();
        }

        public async Task<List<Album>> GetAll()
        {
            return await _musicDbContext.Albums.Include(x => x.Songs)
                .ThenInclude(x=> x.Genre)
                .Include(x=> x.Songs)
                .ThenInclude(x=>x.Playlist)
                .ToListAsync();
        }

        public async Task<Album> GetById(int id)
        {
            return await _musicDbContext.Albums.Include(x => x.Songs)
                .ThenInclude(x => x.Genre)
                .Include(x => x.Songs)
                .ThenInclude(x => x.Playlist).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save()
        {
            await _musicDbContext.SaveChangesAsync();
        }

        public async Task Update(Album entity)
        {
            _musicDbContext.Albums.Update(entity);
            await Save();
        }
    }
}

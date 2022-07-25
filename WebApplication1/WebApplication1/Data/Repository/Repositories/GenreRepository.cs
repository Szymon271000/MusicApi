using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data.Repository.Interface
{
    public class GenreRepository : IBaseRepository<Genre>
    {
        private readonly MusicDbContext _musicDbContext;

        public GenreRepository(MusicDbContext context)
        {
            _musicDbContext = context;
        }

        public async Task Add(Genre entity)
        {
            await _musicDbContext.Genres.AddAsync(entity);
            await Save();
        }

        public async Task Delete(Genre entity)
        {
            _musicDbContext.Genres.Remove(entity);
            await Save();
        }

        public async Task<List<Genre>> GetAll()
        {
            return await _musicDbContext.Genres.ToListAsync();
        }

        public async Task<Genre> GetById(int id)
        {
            return await _musicDbContext.Genres.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save()
        {
            await _musicDbContext.SaveChangesAsync();
        }

        public async Task Update(Genre entity)
        {
            _musicDbContext.Genres.Update(entity);
            await Save();
        }
    }
}

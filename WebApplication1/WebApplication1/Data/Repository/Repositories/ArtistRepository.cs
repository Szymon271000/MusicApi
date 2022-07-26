using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Repository.Interface;
using WebApplication1.Models;

namespace WebApplication1.Data.Repository.Repositories
{
    public class ArtistRepository : IBaseRepository<Artist>
    {
        private readonly MusicDbContext _musicDbContext;

        public ArtistRepository(MusicDbContext context)
        {
            _musicDbContext = context;
        }

        public async Task Add(Artist entity)
        {
            await _musicDbContext.Artists.AddAsync(entity);
            await Save();
        }

        public async Task Delete(Artist entity)
        {
            _musicDbContext.Artists.Remove(entity);
            await Save();
        }

        public async Task<List<Artist>> GetAll()
        {
            return await _musicDbContext.Artists.Include(x=> x.Albums).ThenInclude(x=> x.Songs).ToListAsync();
        }

        public async Task<Artist> GetById(int id)
        {
            return await _musicDbContext.Artists.Include(x => x.Albums).ThenInclude(x => x.Songs).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save()
        {
            await _musicDbContext.SaveChangesAsync();

        }

        public async Task Update(Artist entity)
        {
            _musicDbContext.Artists.Update(entity);
            await Save();
        }
    }
}

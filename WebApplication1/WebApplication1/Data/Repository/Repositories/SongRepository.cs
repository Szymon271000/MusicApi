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
        public Task Add(Song entity)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Song entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Song>> GetAll()
        {
            return await _musicDbContext.Songs.ToListAsync();
        }

        public Task<Song> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public Task Update(Song entity)
        {
            throw new NotImplementedException();
        }
    }
}



using WebApplication1.Dtos.AlbumDto;
using WebApplication1.Models;

namespace WebApplication1.Dtos.NewFolder
{
    public class SongDtoToView
    {
        public string? Name { get; set; }
        public int? AlbumId { get; set; }

        public int? GenreId { get; set; }

    }
}

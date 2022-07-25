using WebApplication1.Models;

namespace WebApplication1.Dtos.NewFolder
{
    public class SongDto
    {
        public string? Name { get; set; }
        public int? AlbumId { get; set; }

        public int? ArtistId { get; set; }

        public int? GenreId { get; set; }
    }
}

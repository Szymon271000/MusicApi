

using WebApplication1.Dtos.AlbumDto;
using WebApplication1.Models;

namespace WebApplication1.Dtos.NewFolder
{
    public class SongDtoToView
    {
        public string? Name { get; set; }

        public string? Album { get; set; }
        public string? Genre { get; set; }
        public string? Playlist { get; set; }
    }
}

using WebApplication1.Dtos.NewFolder;
using WebApplication1.Models;

namespace WebApplication1.Dtos.PlaylistDto
{
    public class PlaylistDto
    {
        public string? Name { get; set; }
        public IEnumerable<SongDtoToView>? Songs { get; set; }

    }
}

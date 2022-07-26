using WebApplication1.Dtos.NewFolder;

namespace WebApplication1.Dtos.AlbumDto
{
    public class AlbumDto
    {
        public string? Name { get; set; }
        public IEnumerable<SongDto>? Songs { get; set; }
    }
}

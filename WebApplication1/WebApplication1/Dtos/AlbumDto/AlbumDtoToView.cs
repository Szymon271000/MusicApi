using WebApplication1.Dtos.NewFolder;

namespace WebApplication1.Dtos.AlbumDto
{
    public class AlbumDtoToView
    {
        public string? Name { get; set; }
        public IEnumerable<SongDto>? Songs { get; set; }
    }
}

using WebApplication1.Dtos.NewFolder;

namespace WebApplication1.Dtos.AlbumDto
{
    public class AlbumDtoToView
    {
        public string? Name { get; set; }
        public IEnumerable<SongDtoToView>? Songs { get; set; }
    }
}

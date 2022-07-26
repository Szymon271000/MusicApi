using WebApplication1.Dtos.AlbumDto;

namespace WebApplication1.Dtos
{
    public class ArtistDtoToView
    {
        public string? Name { get; set; }
        public IEnumerable<AlbumDtoToView>? Albums { get; set; }

    }
}

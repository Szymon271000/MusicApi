using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public IEnumerable<Album> ?Albums { get; set; }

        [ForeignKey("Album")]
        public int? AlbumId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        
        public Genre? Genre { get; set; }
        [ForeignKey("Genre")]
        public int ? GenreId { get; set; }

        public Album? Album { get; set; }
        [ForeignKey("Album")]
        public int? AlbumId { get; set; }

        public Playlist? Playlist { get; set; }
        [ForeignKey("Playlist")]
        public int? PlaylistId { get; set; }

    }
}

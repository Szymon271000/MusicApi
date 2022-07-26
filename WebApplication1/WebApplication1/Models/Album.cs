
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public IEnumerable<Song>? Songs { get; set; }

        [ForeignKey("Song")]
        public int? SongId { get; set; }
    }
}


namespace WebApplication1.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<Song>? Songs { get; set; }

        public Artist ?Artist { get; set; }
    }
}

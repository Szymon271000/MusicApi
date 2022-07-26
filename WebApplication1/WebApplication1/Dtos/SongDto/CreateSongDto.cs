using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.SongDto
{
    public class CreateSongDto
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
    }
}

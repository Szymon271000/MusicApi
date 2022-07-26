using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.SongDto
{
    public class UpdateSongDto
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
    }
}

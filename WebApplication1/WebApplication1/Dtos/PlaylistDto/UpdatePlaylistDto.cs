using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.PlaylistDto
{
    public class UpdatePlaylistDto
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
    }
}

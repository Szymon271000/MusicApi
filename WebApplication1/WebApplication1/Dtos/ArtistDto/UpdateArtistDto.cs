using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.ArtistDto
{
    public class UpdateArtistDto
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
    }
}

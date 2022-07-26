using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.AlbumDto
{
    public class CreatedAlbumDto
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
    }
}

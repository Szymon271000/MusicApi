using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.GenreDto
{
    public class UpdateGenreDto
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
    }
}

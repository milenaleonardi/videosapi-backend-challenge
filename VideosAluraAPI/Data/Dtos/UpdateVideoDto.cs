using System.ComponentModel.DataAnnotations;

namespace VideosAluraAPI.Data.Dtos
{
    public class UpdateVideoDto
    {
        [Required]
        public string? Titulo { get; set; }
        [Required]
        public string? Descricao { get; set; }
        [Required]
        public string? Url { get; set; }
        [Required]
        public int CategoriaId { get; set; }
    }
}

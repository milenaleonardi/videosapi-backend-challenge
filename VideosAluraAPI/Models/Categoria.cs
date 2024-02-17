using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VideosAluraAPI.Models
{
    public class Categoria
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Titulo é obrigatório")]
        public string? Titulo { get; set; }
        [Required(ErrorMessage = "O campo Cor é obrigatório")]
        public string? Cor { get; set; }
        public virtual ICollection<Video>? Videos { get; set; }
    }
}

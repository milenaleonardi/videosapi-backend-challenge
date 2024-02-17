using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VideosAluraAPI.Models
{
    public class Video
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Titulo é obrigatório.")]
        [StringLength(50)]
        public string? Titulo { get; set; }
        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        [StringLength(250)]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "A URL do vídeo é obrigatória.")]
        [DataType(DataType.Url)]
        public string? Url { get; set; }
        [Required]
        public int? CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VideosAluraAPI.Data.Dtos
{
    public class CreateCategoriaDto
    {
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Cor { get; set; }
    }
}

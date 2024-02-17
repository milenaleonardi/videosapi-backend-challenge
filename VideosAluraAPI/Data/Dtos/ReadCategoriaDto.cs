
using VideosAluraAPI.Models;

namespace VideosAluraAPI.Data.Dtos
{
    public class ReadCategoriaDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cor { get; set; }
        public virtual ICollection<Video> Video { get; set; }
    }
}

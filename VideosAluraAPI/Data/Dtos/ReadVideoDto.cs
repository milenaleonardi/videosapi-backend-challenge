namespace VideosAluraAPI.Data.Dtos
{
    public class ReadVideoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Url { get; set; }
        public int CategoriaId { get; set; }
    }
}

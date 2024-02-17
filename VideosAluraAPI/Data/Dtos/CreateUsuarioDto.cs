using System.ComponentModel.DataAnnotations;

namespace VideosAluraAPI.Data.Dtos
{
    public class CreateUsuarioDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [Compare("Password")]
        public string? ValidatePassword { get; set; }
    }
}

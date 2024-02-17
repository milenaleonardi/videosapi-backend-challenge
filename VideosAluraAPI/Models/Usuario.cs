using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VideosAluraAPI.Models
{
    public class Usuario : IdentityUser
    {
        public Usuario() : base()
        {
        }
    }
}

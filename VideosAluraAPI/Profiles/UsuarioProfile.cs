using AutoMapper;
using VideosAluraAPI.Data.Dtos;
using VideosAluraAPI.Models;

namespace VideosAluraAPI.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
        }
    }
}

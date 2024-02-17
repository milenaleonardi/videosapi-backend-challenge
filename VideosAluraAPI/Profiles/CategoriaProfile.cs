using AutoMapper;
using VideosAluraAPI.Data.Dtos;
using VideosAluraAPI.Models;

namespace VideosAluraAPI.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CreateCategoriaDto, Categoria>();
            CreateMap<UpdateCategoriaDto, Categoria>();
            CreateMap<Categoria, ReadCategoriaDto>();
        }
    }
}

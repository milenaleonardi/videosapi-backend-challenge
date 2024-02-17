using AutoMapper;
using VideosAluraAPI.Data.Dtos;
using VideosAluraAPI.Models;

namespace VideosAluraAPI.Profiles
{
    public class VideoProfile : Profile
    {
        public VideoProfile()
        {
            CreateMap<CreateVideoDto, Video>(); 
            CreateMap<UpdateVideoDto, Video>();
            CreateMap<Video, ReadVideoDto > ();
        }
    }
}

using AutoMapper;
using CleanArchitecture.Application.Features.Directors.Command.CreateDirector;
using CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;
using CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArquitecture.Domain;

namespace CleanArchitecture.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Video, GetVideosListQueryResponse>();
            CreateMap<CreateStreamerCommand, Streamer>();
            CreateMap<DeleteStreamerCommand, Streamer>();
            CreateMap<CreateDirectorCommand, Director>();
            CreateMap<UpdateStreamerCommand, Streamer>();
        }
    }
}

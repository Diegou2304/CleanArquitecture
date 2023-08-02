using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{
    public class GetVideosListQueryHandler : IRequestHandler<GetVideosListQuery, List<GetVideosListQueryResponse>>
    {
        //private readonly IVideoRepository _videoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVideosListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetVideosListQueryResponse>> Handle(GetVideosListQuery request, CancellationToken cancellationToken)
        {
            var videos = await _unitOfWork.VideoRepository.GetVideoByUsername(request.Username);


            return _mapper.Map<List<GetVideosListQueryResponse>>(videos);
        }

    }
}

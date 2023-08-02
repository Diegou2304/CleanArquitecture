

using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArquitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;


namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer
{
    public class DeleteStreamerHandler : IRequestHandler<DeleteStreamerCommand>
    {
        //private readonly IStreamerRepository _streamerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteStreamerHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteStreamerHandler(IUnitOfWork unitOfWork,               
                                    ILogger<DeleteStreamerHandler> logger,
                                    IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamer = await _unitOfWork.StreamerRepository.GetByIdAsync(request.Id);
            

            if (streamer == null)
            {
                _logger.LogError($"No se encontró registro alguno del streamer {request.Id}");
                throw new NotFoundException(nameof(streamer), request.Id);
            }



             _unitOfWork.StreamerRepository.DeleteEntity(streamer);

            await _unitOfWork.Complete();
            _logger.LogInformation($"La operación fue exitosa eliminando el streamer {request.Id}");

        }
    }
}

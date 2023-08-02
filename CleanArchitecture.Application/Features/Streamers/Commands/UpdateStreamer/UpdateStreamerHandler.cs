using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArquitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;


namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerHandler : IRequestHandler<UpdateStreamerCommand>
    {
        //private readonly IStreamerRepository _streamerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateStreamerHandler> _logger;

        public UpdateStreamerHandler(IUnitOfWork unitOfWork, 
                                     IMapper mapper, 
                                     ILogger<UpdateStreamerHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            //var streamer = await _streamerRepository.GetByIdAsync(request.Id);

            var streamer = await _unitOfWork.StreamerRepository.GetByIdAsync(request.Id);


            if (streamer == null)
            {

                _logger.LogError($"No se encontró el streamer id {request.Id}");
                throw new NotFoundException(nameof(streamer), request.Id);
            }

            _mapper.Map(request, streamer, typeof(UpdateStreamerCommand), typeof(Streamer));

             _unitOfWork.StreamerRepository.UpdateEntity(streamer);

            await _unitOfWork.Complete();

            _logger.LogInformation($"La operación fue exitosa actualizando el streamer {request.Id}");

            
        }

        
    }
}

using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArquitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;


namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {
        //private readonly IStreamerRepository _streamerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateStreamerCommandHandler> _logger;

        public CreateStreamerCommandHandler(IStreamerRepository streamerRepository,
                                      IMapper mapper,
                                      IEmailService emailService,
                                      ILogger<CreateStreamerCommandHandler> logger,
                                      IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerEntity = _mapper.Map<Streamer>(request);
            _unitOfWork.StreamerRepository.AddEntity(streamerEntity);

            var result = await _unitOfWork.Complete();

            if(result <= 0)
            {
#pragma warning disable S112 // General exceptions should never be thrown
                throw new Exception($"No se pudo insertar el record de streamer");
#pragma warning restore S112 // General exceptions should never be thrown
            }
            _logger.LogInformation($"Streamer {streamerEntity.Id} fue creado exitosamente");

            await SendEmail(streamerEntity);

            return streamerEntity.Id;
        }

        private async Task SendEmail(Streamer streamer)
        {
            var email = new Email
            {
                To = "diegou230498@gmail.com",
                Body = "La compania de streamer se creo correctamente",
                Subject = "Mensaje de alerta"

            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo enviar el email exitosamente al Streamer {streamer.Id}");
            }

        }
    }
}

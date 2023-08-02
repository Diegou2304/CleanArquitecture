using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArquitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;


namespace CleanArchitecture.Application.Features.Directors.Command.CreateDirector
{
    public class CreateDirectorHandler : IRequestHandler<CreateDirectorCommand, int>
    {
        private readonly ILogger<CreateDirectorHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDirectorHandler(ILogger<CreateDirectorHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
        {
            var directorEntity = _mapper.Map<Director>(request);

            _unitOfWork.Repository<Director>().AddEntity(directorEntity);

            var result = await _unitOfWork.Complete();

            if (result <= 0) 
            {
                _logger.LogError("No se insertó el record del director");
                throw new Exception("No se pudo insertar el record del director");
        
            }

            return directorEntity.Id;
            
        }
    }
}

using Aplication.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Features.CostumerTypes;

public class CreateCostumerType
{
    public class CreateCostumerTypeCommand : IRequest<CostumerType>
    {
        public string Description { get; set; }
    }

    public class CreateCostumerTypeCommandValidator : AbstractValidator<CreateCostumerTypeCommand>
    {
        public CreateCostumerTypeCommandValidator()
        {
            RuleFor(x => x.Description).NotEmpty().NotNull();
        }
    }
    
    public class CreateCostumerTypeCommandHandler: IRequestHandler<CreateCostumerTypeCommand,CostumerType>
    {
        private readonly DataContext _context;
        private readonly IGenericRepository<CostumerType> _genericRepository;
        private readonly UserManager<User> _userManager;
        private readonly IUserAcessor _userAccessor;

        public CreateCostumerTypeCommandHandler(DataContext context, IGenericRepository<CostumerType> genericRepository,
            UserManager<User> userManager,
            IUserAcessor userAccessor)
        {
            _context = context;
            _genericRepository = genericRepository;
            _userManager = userManager;
            _userAccessor = userAccessor;
        }

        public async Task<CostumerType> Handle(CreateCostumerTypeCommand request, CancellationToken cancellationToken)
        {
            var costumerType = new CostumerType()
            {
                Description = request.Description,
                CreatedByUserId = _userAccessor.GetCurrentUserId()
            };
            
            _genericRepository.Add(costumerType);
            
            var result = await _genericRepository.Complete() < 0;
            
            if (result)
            {
                throw new Exception("AN ERROR OCCURRED");
            }

            return costumerType;
        }
    }
}
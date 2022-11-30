using Aplication.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Features.Costumers;

public class CreateCostumer
{
    public class CreateCostumerCommand: IRequest<Costumer>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public int CostumerTypeId { get; set; }
    }
    
    public class CreateCostumerCommandValidator: AbstractValidator<CreateCostumerCommand>
    {
        public CreateCostumerCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull();
        }
    }
    
    public class CreateCostumerCommandHandler: IRequestHandler<CreateCostumerCommand, Costumer>
    {
        private readonly DataContext _context;
        private readonly IGenericRepository<Costumer> _genericRepository;

        public CreateCostumerCommandHandler(DataContext context, IGenericRepository<Costumer> genericRepository)
        {
            _context = context;
            _genericRepository = genericRepository;
        }

        public async Task<Costumer> Handle(CreateCostumerCommand request, CancellationToken cancellationToken)
        {
            var costumerType = await _context.CostumerTypes.FindAsync(request.CostumerTypeId);

            if (costumerType is null)
            {
                throw new Exception("Costumer Type is not found");
            }

            var costumer = new Costumer()
            {
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                CostumerTypeId = request.CostumerTypeId
            };
            switch (costumerType.Description)
            {
                case "Fisico":
                    costumer.CPF = request.CPF;
                    costumer.CNPJ = null;
                    break;
                
                case "Juridico":
                    costumer.CPF = null;
                    costumer.CNPJ = request.CNPJ;
                    break;
                default:
                    costumer = null;
                    break;
            }

            if (costumer is null)
            {
                throw new Exception("Fail to create Clients");
            }
            _genericRepository.Add(costumer);
            
            var result = await _genericRepository.Complete() < 0;
            
            if (result)
            {
                throw new Exception("AN ERROR OCCURRED");
            }

            return costumer;
        }
    }
}
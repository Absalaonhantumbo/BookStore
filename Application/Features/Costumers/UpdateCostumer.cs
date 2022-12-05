using System.Net;
using Aplication.Errors;
using Aplication.Interfaces;
using Application.Features.Costumers.Specifications;
using Application.Features.CostumerTypes.Specifications;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Features.Costumers;

public class UpdateCostumer
{
    public class UpdateCostumerCommand: IRequest<Costumer>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public int CostumerTypeId { get; set; }
    }
    
    public class UpdateCostumerCommandValidators: AbstractValidator<UpdateCostumerCommand>
    {
        public UpdateCostumerCommandValidators()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.PhoneNumber).NotNull();
            RuleFor(x => x.CostumerTypeId).NotNull();
        }
    }
    
    public class UpdateCostumerCommandHandler: IRequestHandler<UpdateCostumerCommand, Costumer>
    {
        private readonly IGenericRepository<Costumer> _genericRepository;
        private readonly DataContext _context;

        public UpdateCostumerCommandHandler(IGenericRepository<Costumer> genericRepository, DataContext context)
        {
            _genericRepository = genericRepository;
            _context = context;
        }

        public async Task<Costumer> Handle(UpdateCostumerCommand request, CancellationToken cancellationToken)
        {
            var spec = new ListCostumerById(request.Id);

            var costumer = await _genericRepository.GetEntityWithSpec(spec);
            if (costumer is null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Costumer Not Found");
            }

            var costumerType = await _context.CostumerTypes.FindAsync(request.CostumerTypeId);
            if (costumerType is null)
            {
                throw new Exception("Costumer Type is not found");
            }

            costumer.Name = request.Name;
            costumer.PhoneNumber = request.PhoneNumber;
            costumer.CostumerTypeId = request.CostumerTypeId;
            
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
                throw new Exception("Fail to Update Costumer");
            }
            _genericRepository.Update(costumer);
            
            var result = await _genericRepository.Complete() < 0;
            
            if (result)
            {
                throw new Exception("AN ERROR OCCURRED");
            }

            return costumer;
        }
    }
}
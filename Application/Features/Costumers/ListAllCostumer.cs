using Aplication.Interfaces;
using Application.Features.Costumers.Specifications;
using Domain;
using MediatR;
using Persistence;

namespace Application.Features.Costumers;

public class ListAllCostumer
{
    public class ListAllCostumerQuery: IRequest<List<Costumer>>
    {
        
    }
    public class ListAllCostumerQueryHandler: IRequestHandler<ListAllCostumerQuery, List<Costumer>>
    {
        private readonly IGenericRepository<Costumer> _genericRepository;

        public ListAllCostumerQueryHandler(IGenericRepository<Costumer> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<List<Costumer>> Handle(ListAllCostumerQuery request, CancellationToken cancellationToken)
        {
            var spec = new ListAllCostumersSpecification();
            
            return await _genericRepository.ListWithSpecAsync(spec);
            
        }
    }
}
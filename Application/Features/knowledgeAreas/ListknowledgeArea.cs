using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Features.knowledgeAreas;

public class ListknowledgeArea
{
    public class ListknowledgeAreaQuery: IRequest<List<knowledgeArea>>
    {

    }
    
    public class ListknowledgeAreaQueryHandler: IRequestHandler<ListknowledgeAreaQuery, List<knowledgeArea>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListknowledgeAreaQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<knowledgeArea>> Handle(ListknowledgeAreaQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<knowledgeArea>().ListAllAsync();
        }
    }
}
using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Features.DeweyDecimalClassifications;

public class ListAllDeweyDecimalClassification
{
    public class ListAllDeweyDecimalClassificationQuery: IRequest<List<DeweyDecimalClassification>>
    {

    }
    
    public class ListAllDeweyDecimalClassificationQueryHandler: IRequestHandler<ListAllDeweyDecimalClassificationQuery, List<DeweyDecimalClassification>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListAllDeweyDecimalClassificationQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<DeweyDecimalClassification>> Handle(ListAllDeweyDecimalClassificationQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<DeweyDecimalClassification>().ListAllAsync();
        }
    }
}
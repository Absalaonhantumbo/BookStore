using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Features.Countries;

public class ListAllCountry
{
    public class ListAllCountryQuery: IRequest<List<Country>>
    {

    }
    
    public class ListAllCountryQueryHandler: IRequestHandler<ListAllCountryQuery, List<Country>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListAllCountryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Country>> Handle(ListAllCountryQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<Country>().ListAllAsync();
        }
    }
}
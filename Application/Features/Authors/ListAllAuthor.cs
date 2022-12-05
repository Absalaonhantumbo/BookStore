using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Features.Authors;

public class ListAllAuthor
{
    public class ListAllAuthorQuery: IRequest<List<Author>>
    {

    }
    
    public class ListAllAuthorQueryHandler: IRequestHandler<ListAllAuthorQuery, List<Author>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListAllAuthorQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Author>> Handle(ListAllAuthorQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<Author>().ListAllAsync();
        }
    }
}
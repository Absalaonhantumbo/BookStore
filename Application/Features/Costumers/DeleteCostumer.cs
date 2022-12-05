using System.Net;
using Aplication.Errors;
using Application.Features.Costumers.Specifications;
using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Features.Costumers;

public class DeleteCostumer
{
    public class DeleteCostumerCommand: IRequest<Costumer>
    {
        public int ConstumerId { get; set; }
        
    }
    
    public class DeleteCostumerCommandHandler: IRequestHandler<DeleteCostumerCommand, Costumer>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCostumerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Costumer> Handle(DeleteCostumerCommand request, CancellationToken cancellationToken)
        {
            var spec = new ListCostumerById(request.ConstumerId);
            var costumer = await _unitOfWork.Repository<Costumer>().GetEntityWithSpec(spec);

            if (costumer is null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Costumer is Not Found");

            }
            _unitOfWork.Repository<Costumer>().Delete(costumer);

            var result = await _unitOfWork.Complete() > 0;
            if (result)
            {
                return costumer;
            }

            throw new Exception("Occurred problems for delete costumer");
        }
    }
}
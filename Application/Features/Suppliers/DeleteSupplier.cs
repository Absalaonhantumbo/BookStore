using System.Net;
using Aplication.Errors;
using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Features.Suppliers;

public class DeleteSupplier
{
    public class DeleteSupplierCommand : IRequest<Supplier>
    {
        public int SupplierId { get; set; }
        
    }

    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, Supplier>
    {
        private readonly IUnitOfWork _unitOfWork;


        public DeleteSupplierCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Supplier> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _unitOfWork.Repository<Supplier>().GetByIdAsync(request.SupplierId);

            if (supplier is null)
                throw new RestException(HttpStatusCode.NotFound, "SupplierNotFoundMessage");

            _unitOfWork.Repository<Supplier>().Delete(supplier);
            var result = await _unitOfWork.Complete() > 0;

            if (result) return supplier;

            throw new Exception("ProblemSaving");
        }
    }
}
using System.Net;
using Aplication.Errors;
using Application.Dtos;
using Application.Features.Suppliers.Specifications.Suppliers;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Suppliers;

public class GetSuppliersById
{
    public class GetSuppliersByIdQuery : IRequest<SuppliersDto>
    {
        public int SupplierId { get; set; }
    }

    public class GetSuppliersByIdQueryHandler : IRequestHandler<GetSuppliersByIdQuery, SuppliersDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSuppliersByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SuppliersDto> Handle(GetSuppliersByIdQuery request, CancellationToken cancellationToken)
        {
            var suppliers = await _unitOfWork.Repository<Supplier>().GetByIdAsync(request.SupplierId);

            if (suppliers is null) throw new RestException(HttpStatusCode.NotFound, $"Supplier not found");

            var spec = new ListAllSupplierSpecifications(request.SupplierId);
            var supplierWithSpec = await _unitOfWork.Repository<Supplier>().GetEntityWithSpec(spec);

            return _mapper.Map<Supplier, SuppliersDto>(supplierWithSpec);
        }
    }
}
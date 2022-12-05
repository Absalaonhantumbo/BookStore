using System.Net;
using Aplication.Errors;
using Application.Dtos;
using Application.Features.Suppliers.Specifications.Suppliers;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.Features.Suppliers;

public class UpdateSupplier
{
    public class UpdateSupplierCommad : IRequest<SuppliersDto>
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int SupplierTypeId { get; set; }
        public int CompanyTypeId { get; set; }
        public string Code { get; set; }
        public string LegalName { get; set; }
        public string TradeName { get; set; }

        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class UpdateSupplierCommadValidator : AbstractValidator<UpdateSupplierCommad>
    {
        public UpdateSupplierCommadValidator()
        {
            RuleFor(x => x.Address).NotEmpty().NotNull();
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.Telephone).NotEmpty();
            RuleFor(x => x.CountryId).NotNull().NotEmpty();
            RuleFor(x => x.LegalName).NotEmpty();
            RuleFor(x => x.TradeName).NotEmpty();
            RuleFor(x => x.CompanyTypeId).NotNull().NotEmpty();
            RuleFor(x => x.SupplierTypeId).NotNull().NotEmpty();
        }
    }

    public class UpdateSupplierCommadHandler : IRequestHandler<UpdateSupplierCommad, SuppliersDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSupplierCommadHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SuppliersDto> Handle(UpdateSupplierCommad request, CancellationToken cancellationToken)
        {
            var supplier = await _unitOfWork.Repository<Supplier>().GetByIdAsync(request.Id);
            if (supplier is null)
                throw new RestException(HttpStatusCode.NotFound, "SupplierNotFound");
            var supplierAtributSpec = new SupplierByCodeSpecifications(request.Code);

            var supplierWithSpec = _unitOfWork.Repository<Supplier>().ListWithSpecAsync(supplierAtributSpec);

            if (supplierWithSpec.Result.Count > 1)
                throw new RestException(HttpStatusCode.Conflict, "SupplierExist");

            supplier.Code = request.Code;
            supplier.Address = request.Address;
            supplier.Email = request.Email;
            supplier.Telephone = request.Telephone;
            supplier.LegalName = request.LegalName;
            supplier.TradeName = request.TradeName;
            supplier.SupplierTypeId = request.SupplierTypeId;
            supplier.CountryId = request.CountryId;
            supplier.CompanyTypeId = request.CompanyTypeId;


            var spec = new ListAllSupplierSpecifications(request.Id);
            var supplierAddInclude = await _unitOfWork.Repository<Supplier>().GetEntityWithSpec(spec);

            _unitOfWork.Repository<Supplier>().Update(supplier);
            var result = await _unitOfWork.Complete() > 0;

            if (result) return _mapper.Map<Supplier, SuppliersDto>(supplier);

            throw new Exception("Could not update Supplier");
        }
    }
}
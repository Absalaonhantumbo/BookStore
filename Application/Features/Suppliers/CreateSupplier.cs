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

public class CreateSupplier
{
    public class CreateSupplierCommand : IRequest<SuppliersDto>
    {
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

    public class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierCommandValidator()
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

    public class CreateSupplierCommandValidatorHandler : IRequestHandler<CreateSupplierCommand, SuppliersDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSupplierCommandValidatorHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SuppliersDto> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var suppliersSpec = new SupplierByCodeSpecifications(request.Code);
            var supplier = await _unitOfWork.Repository<Supplier>().GetEntityWithSpec(suppliersSpec);

            if (supplier is not null)
            {
                throw new RestException(HttpStatusCode.Conflict,"SuppliersExistMessage");
            }

            supplier = new Supplier()
            {
                Code = request.Code,
                Address = request.Address,
                Email = request.Email,
                Telephone = request.Telephone,
                LegalName = request.LegalName,
                TradeName = request.TradeName,
                SupplierTypeId = request.SupplierTypeId,
                CountryId = request.CountryId,
                CompanyTypeId = request.CompanyTypeId
            };
            _unitOfWork.Repository<Supplier>().Add(supplier);

            var result = await _unitOfWork.Complete() > 0;
            if (result)
            {
              return  _mapper.Map<Supplier, SuppliersDto>(supplier);
            }

            throw new Exception("ProblemSaving");
        }
    }
}
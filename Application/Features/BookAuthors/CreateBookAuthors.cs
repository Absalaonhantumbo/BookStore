using System.Net;
using Aplication.Errors;
using Application.Dtos;
using Application.Features.BookAuthors.Specifications;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Features.BookAuthors;

public class CreateBookAuthors
{
    public class CreateBookAuthorsCommand: IRequest<BookAuthorDto>
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string SubjectMatter { get; set; }
        public int QuantityStock { get; set; }
        public int Edition { get; set; }
        public int Page { get; set; }
        public int PublishingCompanyId { get; set; }
        public int DeweyDecimalClassificationId { get; set; }
        public int SupplierId { get; set; }
    
        public List<AuthorBooksModelDto> Authors { get; set; } = new List<AuthorBooksModelDto>();
    }
    
    public class CreateBookAuthorsCommandValidators: AbstractValidator<CreateBookAuthorsCommand>
    {
        public CreateBookAuthorsCommandValidators()
        {
            RuleFor(x => x.ISBN).NotNull().NotEmpty();
            RuleFor(x => x.Title).NotNull().NotEmpty();
            RuleFor(x => x.SubjectMatter).NotNull().NotEmpty();
            RuleFor(x => x.QuantityStock).NotNull().NotEmpty();
            RuleFor(x => x.Edition).NotNull().NotEmpty();
            RuleFor(x => x.Page).NotNull().NotEmpty();
            RuleFor(x => x.PublishingCompanyId).NotNull().NotEmpty();
            RuleFor(x => x.DeweyDecimalClassificationId).NotNull().NotEmpty();
            RuleFor(x => x.SupplierId).NotNull().NotEmpty();
        }
    }
    
    public class CreateBookAuthorsCommandHandler: IRequestHandler<CreateBookAuthorsCommand, BookAuthorDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _context;

        public CreateBookAuthorsCommandHandler(IUnitOfWork unitOfWork, DataContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        
        public async Task<BookAuthorDto> Handle(CreateBookAuthorsCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _unitOfWork.Repository<Supplier>().GetByIdAsync(request.SupplierId);

            if (supplier is null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Supplier is not found");
            }
            
            var deweyDecimalClassification =await _unitOfWork.Repository<DeweyDecimalClassification>().GetByIdAsync(request.DeweyDecimalClassificationId);

            if (deweyDecimalClassification is null)
            {
                throw new RestException(HttpStatusCode.NotFound, "deweyDecimalClassification is not found");
            }
            
            var publishingCompany =await _unitOfWork.Repository<PublishingCompany>().GetByIdAsync(request.PublishingCompanyId);

            if (publishingCompany is null)
            {
                throw new RestException(HttpStatusCode.NotFound, "PublishingCompany is not found");
            }
            
            var bookAuthorDto = new BookAuthorDto();
            
            var listOfAuthorsModel = request.Authors;

            if (listOfAuthorsModel.Count == 0)
            {
                throw new RestException(HttpStatusCode.Forbidden, "Empty Book List Given Exception");
            }
 
            // Validations
            var listOfAuthorsToValidateId = listOfAuthorsModel.Select(x => x.Id).ToList();

            var listOfAuthorsToValidateSpecification = new GetListOfAuthorsById(listOfAuthorsToValidateId);
            
            var listOfAuthorsToValidate = await _unitOfWork.Repository<Author>()
                .ListWithSpecAsync(listOfAuthorsToValidateSpecification);

            if (listOfAuthorsToValidate.Count == 0)
            {
                throw new RestException(HttpStatusCode.NotFound,"Given List Of Authors Contains A Invalid Authors");
            }

            await using var transactionScope = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var book = new Book()
                {
                    ISBN = request.ISBN,
                    Title = request.Title,
                    SubjectMatter = request.SubjectMatter,
                    QuantityStock = request.QuantityStock,
                    Edition = request.Edition,
                    Page = request.Page,
                    Balance = request.QuantityStock,
                    PublishingCompanyId = request.PublishingCompanyId,
                    DeweyDecimalClassificationId = request.DeweyDecimalClassificationId,
                    SupplierId = request.SupplierId
                };
                _unitOfWork.Repository<Book>().Add(book);
                
                var result = await _unitOfWork.Complete() < 0;
                if (result)
                {
                    throw new Exception("Error Saving Book");
                }

                bookAuthorDto.ISBN = book.ISBN;
                bookAuthorDto.Title = book.Title;
                bookAuthorDto.SubjectMatter = book.SubjectMatter;
                bookAuthorDto.QuantityStock = book.QuantityStock;
                bookAuthorDto.Edition = book.Edition;
                bookAuthorDto.Page = book.Page;
                bookAuthorDto.PublishingCompanyId = book.PublishingCompanyId;
                bookAuthorDto.DeweyDecimalClassificationId = book.DeweyDecimalClassificationId;
                bookAuthorDto.SupplierId = book.SupplierId;
                bookAuthorDto.PublishingCompany = book.PublishingCompany.Name;
                bookAuthorDto.DeweyDecimalClassification = book.DeweyDecimalClassification.Name;
                bookAuthorDto.Supplier = book.Supplier.LegalName;
                
                // Add Authors
                foreach (var author in listOfAuthorsModel)
                {
                    var authorToUpdate = await _unitOfWork.Repository<Author>().GetByIdAsync(author.Id);

                    var bookAuthors = new AuthorBook()
                    {
                        AuthorId = author.Id,
                        BookId = book.Id
                    };
                    _unitOfWork.Repository<AuthorBook>().Add(bookAuthors);
                    
                    bookAuthorDto.Authors.Add(new AuthorBooks
                    {
                        Id = authorToUpdate.Id
                    });
                }
                
                result = await _unitOfWork.Complete() < 0;
                if (result)
                {
                    throw new Exception("problem saving");
                }

                await transactionScope.CommitAsync(cancellationToken);
                return bookAuthorDto;

            }
            catch (Exception e)
            {
                Console.WriteLine("System Error {0}", e.Message);
                await transactionScope.RollbackAsync(cancellationToken);
                throw;
            }

        }
    }
}
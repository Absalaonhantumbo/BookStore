using System.Net;
using Aplication.Errors;
using Application.Dtos;
using Application.Features.BookAuthors.Specifications;
using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Features.BookAuthors;

public class GetBookAuthorByBookId
{
    public class GetBookAuthorByBookIdQuery: IRequest<BookAuthorDto>
    {
        public int BookId { get; set; }
    }
    
    public class GetBookAuthorByBookIdQueryHandler: IRequestHandler<GetBookAuthorByBookIdQuery, BookAuthorDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IListOfAuthorsService _listOfAuthorsService;

        public GetBookAuthorByBookIdQueryHandler(IUnitOfWork unitOfWork, IListOfAuthorsService listOfAuthorsService)
        {
            _unitOfWork = unitOfWork;
            _listOfAuthorsService = listOfAuthorsService;
        }
        
        public async Task<BookAuthorDto> Handle(GetBookAuthorByBookIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetBookByBookIdSpecification(request.BookId);
            var bookAuthorBook = await _unitOfWork.Repository<AuthorBook>().GetEntityWithSpec(spec);
            if (bookAuthorBook is null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Book Does Not Exists");
            }

            var book = bookAuthorBook.Book;
            
            var data = new BookAuthorDto()
            {
                Id = book.Id,
                Title = book.Title,
                SubjectMatter = book.SubjectMatter,
                ISBN = book.ISBN,
                Edition = book.Edition,
                PublishingCompany = book.PublishingCompany.Name,
                PublishingCompanyId = book.PublishingCompanyId,
                DeweyDecimalClassification = book.DeweyDecimalClassification.Name,
                DeweyDecimalClassificationId = book.DeweyDecimalClassificationId,
                Authors = _listOfAuthorsService.GetAuthors(book.Id),
                SupplierId = book.SupplierId,
                QuantityStock = book.QuantityStock,
                Page = book.Page,
                Supplier = book.Supplier.LegalName,
                ImageBook = book.ImageBook
            };

            return data;
        }
        
    }
}
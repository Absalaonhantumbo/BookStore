using System.Net;
using Aplication.Errors;
using Application.Dtos;
using Application.Features.BookAuthors.Specifications;
using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Features.BookAuthors;

public class GetBookByAuthorId
{
    public class GetBookByAuthorIdQuery : IRequest<List<BookAuthorDto>>
    {
        public int AuthorId { get; set; }
    }
    
    public class GetBookByAuthorIdQueryHandler: IRequestHandler<GetBookByAuthorIdQuery, List<BookAuthorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IListOfAuthorsService _listOfAuthorsService;

        public GetBookByAuthorIdQueryHandler(IUnitOfWork unitOfWork, IListOfAuthorsService listOfAuthorsService)
        {
            _unitOfWork = unitOfWork;
            _listOfAuthorsService = listOfAuthorsService;
        }
       
        public async Task<List<BookAuthorDto>> Handle(GetBookByAuthorIdQuery request, CancellationToken cancellationToken)
        {

            var authorSpec = new ListAllBookByAuthorIdSpecification(request.AuthorId);
            var authorExist = await _unitOfWork.Repository<AuthorBook>().GetEntityWithSpec(authorSpec);
            
            if (authorExist is null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Author Does Not Exists");
            }
            var author = await _unitOfWork.Repository<AuthorBook>().ListWithSpecAsync(authorSpec);

            var data = author
                .Select(x => new BookAuthorDto
                {
                    Id = x.Book.Id,
                    Title = x.Book.Title,
                    SubjectMatter = x.Book.SubjectMatter,
                    ISBN = x.Book.ISBN,
                    Edition = x.Book.Edition,
                    PublishingCompany = x.Book.PublishingCompany.Name,
                    PublishingCompanyId = x.Book.PublishingCompanyId,
                    DeweyDecimalClassification = x.Book.DeweyDecimalClassification.Name,
                    DeweyDecimalClassificationId = x.Book.DeweyDecimalClassificationId,
                    Authors = _listOfAuthorsService.GetAuthors(x.Book.Id),
                    SupplierId = x.Book.SupplierId,
                    QuantityStock = x.Book.QuantityStock,
                    Page = x.Book.Page,
                    Supplier = x.Book.Supplier.LegalName,
                    ImageBook = x.Book.ImageBook
                    
                }).ToList();

            return data;
        }
        
    }
}